using SharpAviReader.Avi;
using SharpAviReader.Riff;
using System;
using System.Collections.Generic;

namespace SharpAviReader;

/// <summary>Information about AVI stream.</summary>
/// <seealso cref="AviReader"/>
public partial class AviStream
{
    /// <summary>Reads information about stream from RIFF file / stream.</summary>
    /// <param name="streamList">List chunk with type <see cref="KnownFourCCs.Lists.Stream"/>.</param>
    /// <returns>Created stream.</returns>
    internal static AviStream Read(RiffListReader streamList)
    {
        var data = new HeaderData(streamList);

        if (data.Header is null)
            throw RiffExceptions.StreamHeaderNotFound(streamList);

        if (data.Header.IsVideoStream)
        {
            if (data.BitmapInfo is null)
                throw RiffExceptions.VideoFormatNotFound(streamList);
            if (data.SuperIndex is null)
                throw RiffExceptions.SuperIndexIsNotFoundForVideoStream(streamList);
            return new Video(data.Header, data.CodecSpecificData, data.SuperIndex, data.BitmapInfo);
        }

        if (data.Header.IsAudioStream)
        {
            if (data.WaveFormat is null)
                throw RiffExceptions.WaveFormatNotFound(streamList);
            return new Audio(data.Header, data.CodecSpecificData, data.SuperIndex, data.WaveFormat);
        }

        return new AviStream(data.Header, data.CodecSpecificData, data.SuperIndex);
    }

    protected private readonly AviStreamHeader header;
    protected private readonly AviSuperIndex? superIndex;
    protected private IReadOnlyList<AviIndexItem> indexItems = Array.Empty<AviIndexItem>();

    protected private AviStream(AviStreamHeader header, byte[]? codecSpecificData, AviSuperIndex? superIndex)
    {
        this.header = header;
        CodecSpecificData = codecSpecificData ?? Array.Empty<byte>();
        this.superIndex = superIndex;
    }

    /// <summary>Contains a FOURCC that specifies the type of the data contained in the stream.</summary>
    /// <seealso cref="KnownFourCCs.StreamTypes"/>
    public FourCC StreamType => header.StreamType;

    /// <summary>Is stream disabled?</summary>
    public bool IsDisabled => (header.Flags & AviStreamHeaderFlags.Disabled) == AviStreamHeaderFlags.Disabled;

    /// <summary>Stream priority.</summary>
    /// <remarks>
    /// For example, in a file with multiple audio streams, the one with the highest priority might be the default stream.
    /// </remarks>
    public int Priority => header.Priority;

    /// <summary>Codec-specific data in binary representation from <see cref="KnownFourCCs.Chunks.StreamHeaderAdditionalData"/> chunk.</summary>
    public byte[] CodecSpecificData { get; }

    /// <summary>Index information: where frame data is located in AVI file.</summary>
    public IReadOnlyList<AviIndexItem> Index => indexItems;

    /// <summary>Maximum size of raw frame data in bytes.</summary>
    public int MaxFrameSize { get; protected set; }

    /// <summary>Reads index information from AVI file.</summary>
    /// <param name="reader"></param>
    internal void ReadIndexItems(RiffFileReader reader)
    {
        if (superIndex is null)
            return;

        var res = new List<AviIndexItem>();
        var maxFrameSize = 0;
        using (reader.RestorePositionAfter())
        {
            foreach (var superIndexEntry in superIndex.Entries)
            {
                using (var indexChunk = reader.OpenChunkAtPosition(superIndexEntry.ChunkOffset))
                {
                    var stdIndex = indexChunk.ReadStdIndex();

                    foreach (var stdIndexEntry in stdIndex.Entries)
                    {
                        res.Add(
                            new AviIndexItem
                            {
                                Offset = stdIndex.BaseOffset + stdIndexEntry.Offset,
                                DataSize = stdIndexEntry.DataSize,
                                IsDeltaFrame = stdIndexEntry.IsDeltaFrame,
                            });
                        if (stdIndexEntry.DataSize > maxFrameSize)
                            maxFrameSize = stdIndexEntry.DataSize;
                    }
                }
            }
        }

        indexItems = res;
        MaxFrameSize = maxFrameSize;
    }
}
