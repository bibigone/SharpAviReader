using System.IO;

namespace SharpAviReader.Riff;

internal abstract class RiffListReaderBase : RiffReaderBase
{
    /// <summary>Size in bytes of ChunkId and ChunkSize fields.</summary>
    public static readonly int ChunkHeaderSize = FourCC.SIZE + sizeof(uint);

    protected RiffListReaderBase(BinaryReader binaryReader, FourCC chunkId, long bodyLength, RiffListReaderBase? parent = null)
        : base(binaryReader, chunkId, bodyLength, parent)
    {}

    public bool IsEndOfList => CurrentLocalPosition >= ContentLength;

    public RiffChunkReader OpenSubChunk(FourCC expectedChunkId = default)
    {
        if (CheckReadingScope && CurrentLocalPosition + ChunkHeaderSize > ContentLength)
            throw RiffExceptions.EndOfList(this);
        var chunkId = BinaryReader.ReadFourCC();
        if (expectedChunkId != KnownFourCCs.None && expectedChunkId != chunkId)
            throw RiffExceptions.UnexpectedChunkId(this, expectedChunkId, chunkId);
        var dataLength = BinaryReader.ReadUInt32();
        return new(chunkId, dataLength, this) { CheckReadingScope = CheckReadingScope };
    }

    public RiffListReader OpenSubList(FourCC expectedListType = default)
        => OpenSubChunk(KnownFourCCs.List).AsList(expectedListType);
}
