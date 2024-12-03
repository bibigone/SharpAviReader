using SharpAviReader.Avi;
using SharpAviReader.Riff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SharpAviReader;

/// <summary>
/// Reads AVI file / stream.
/// </summary>
public class AviReader : IDisposable
{
    private readonly RiffFileReader riffFileReader;
    private readonly AviMainHeader aviMainHeader;
    private readonly AviStream[] aviStreams;

    /// <summary>Frame rate from AVI header.</summary>
    public double FramesPerSecond => aviMainHeader.FramesPerSecond;

    /// <summary>Total frame count from AVI header.</summary>
    public int FrameCount => aviMainHeader.TotalFrames;

    /// <summary>AVI stream count.</summary>
    public int StreamCount => aviMainHeader.StreamCount;

    /// <summary>Video width in pixels from AVI header.</summary>
    public int VideoWidth => aviMainHeader.VideoWidth;

    /// <summary>Video height in pixels from AVI header.</summary>
    public int VideoHeight => aviMainHeader.VideoHeight;

    /// <summary>Information about AVI streams.</summary>
    public IReadOnlyList<AviStream> AviStreams => aviStreams;

    /// <summary>Creates new instance of AVI reader for stream specified.</summary>
    /// <param name="stream">Source stream with AVI data. Should support <c>read</c> and <c>seek</c> operations.</param>
    /// <param name="leaveStreamOpen">Leave <paramref name="stream"/> opened after disposing?</param>
    /// <param name="checkConsistency"><see langword="true"/> to check consistency of AVI structure during reading.</param>
    public AviReader(Stream stream, bool leaveStreamOpen = false, bool checkConsistency = true)
    {
        riffFileReader = new RiffFileReader(stream, leaveStreamOpen) { CheckReadingScope = checkConsistency };
        ReadHeader(out aviMainHeader, out aviStreams);
        foreach (var aviStream in aviStreams)
            aviStream.ReadIndexItems(riffFileReader);
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>>
    public void Dispose()
        => riffFileReader.Dispose();

    /// <summary>Reads raw data of the frame specified. Decoding and interpreting of data must be done outside.</summary>
    /// <param name="aviStreamIndex">Zero-based index of AVI stream.</param>
    /// <param name="frameIndex">Zero-based index of AVI frame.</param>
    /// <param name="destination">Destination buffer.</param>
    /// <returns>The total number of bytes read to <paramref name="destination"/> buffer.</returns>
    public int ReadFrameData(int aviStreamIndex, int frameIndex, Span<byte> destination)
    {
        var aviStream = aviStreams[aviStreamIndex];
        var indexItem = aviStream.Index[frameIndex];
        var buffer = destination.Slice(0, indexItem.DataSize);
        var s = riffFileReader.BinaryReader.BaseStream;
        s.Seek(indexItem.Offset, SeekOrigin.Begin);
        return s.Read(buffer);
    }

    /// <summary>Asynchronously reads raw data of the frame specified. Decoding and interpreting of data must be done outside.</summary>
    /// <param name="aviStreamIndex">Zero-based index of AVI stream.</param>
    /// <param name="frameIndex">Zero-based index of AVI frame.</param>
    /// <param name="destination">Destination buffer.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task that represents the asynchronous read operation. Result equals to the total number of bytes read to <paramref name="destination"/> buffer.</returns>
    public ValueTask<int> ReadFrameDataAsync(int aviStreamIndex, int frameIndex, Memory<byte> destination, CancellationToken cancellationToken = default)
    {
        var aviStream = aviStreams[aviStreamIndex];
        var indexItem = aviStream.Index[frameIndex];
        var buffer = destination.Slice(0, indexItem.DataSize);
        var s = riffFileReader.BinaryReader.BaseStream;
        s.Seek(indexItem.Offset, SeekOrigin.Begin);
        return s.ReadAsync(buffer, cancellationToken);
    }

    /// <summary>Reads raw data of the frame specified. Decoding and interpreting of data must be done outside.</summary>
    /// <param name="aviStreamIndex">Zero-based index of AVI stream.Zero-based index of AVI stream.</param>
    /// <param name="frameIndex">Zero-based index of AVI frame.</param>
    /// <param name="destination">Destination buffer.</param>
    /// <param name="startIndex">The zero-based byte offset in <paramref name="destination"/> buffer at which to begin storing the data read.</param>
    /// <param name="maxLength">The maximum number of bytes to be read.</param>
    /// <returns>The total number of bytes read to <paramref name="destination"/> buffer.</returns>
    /// <exception cref="ArgumentException">Invalid values of <paramref name="startIndex"/> or <paramref name="maxLength"/>.</exception>
    public int ReadFrameData(int aviStreamIndex, int frameIndex, byte[] destination, int startIndex, int maxLength)
    {
        var aviStream = aviStreams[aviStreamIndex];
        var indexItem = aviStream.Index[frameIndex];
        var count = indexItem.DataSize;
        if (count > maxLength)
            throw new ArgumentException();
        var s = riffFileReader.BinaryReader.BaseStream;
        s.Seek(indexItem.Offset, SeekOrigin.Begin);
        return s.Read(destination, startIndex, count);
    }

    /// <summary>Asynchronously reads raw data of the frame specified. Decoding and interpreting of data must be done outside.</summary>
    /// <param name="aviStreamIndex">Zero-based index of AVI stream.</param>
    /// <param name="frameIndex">Zero-based index of AVI frame.</param>
    /// <param name="destination">Destination buffer.</param>
    /// <param name="startIndex">The zero-based byte offset in <paramref name="destination"/> buffer at which to begin storing the data read.</param>
    /// <param name="maxLength">The maximum number of bytes to be read.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A task that represents the asynchronous read operation. Result equals to the total number of bytes read to <paramref name="destination"/> buffer.</returns>
    /// <exception cref="ArgumentException">Invalid values of <paramref name="startIndex"/> or <paramref name="maxLength"/>.</exception>
    public Task<int> ReadFrameDataAsync(int aviStreamIndex, int frameIndex, byte[] destination, int startIndex, int maxLength, CancellationToken cancellationToken = default)
    {
        var aviStream = aviStreams[aviStreamIndex];
        var indexItem = aviStream.Index[frameIndex];
        var count = indexItem.DataSize;
        if (count > maxLength)
            throw new ArgumentException();
        var s = riffFileReader.BinaryReader.BaseStream;
        s.Seek(indexItem.Offset, SeekOrigin.Begin);
        return s.ReadAsync(destination, startIndex, count, cancellationToken);
    }

    private void ReadHeader(out AviMainHeader mainHeader, out AviStream[] streams)
    {
        using (var riffChunk = riffFileReader.OpenSubChunk(KnownFourCCs.Riff).AsList(KnownFourCCs.Lists.Avi))
        {
            using (var topListChunk = riffChunk.OpenSubList(KnownFourCCs.Lists.Header))
            {
                using (var headerChunk = topListChunk.OpenSubChunk(KnownFourCCs.Chunks.AviHeader))
                {
                    mainHeader = headerChunk.ReadAviHeader();
                }

                streams = new AviStream[mainHeader.StreamCount];
                for (var streamIndex = 0; streamIndex < mainHeader.StreamCount; streamIndex++)
                {
                    using (var streamList = topListChunk.OpenSubList(KnownFourCCs.Lists.Stream))
                    {
                        streams[streamIndex] = AviStream.Read(streamList);
                    }
                }
            }
        }
    }
}
