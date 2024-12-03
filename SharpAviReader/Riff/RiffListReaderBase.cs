using System.IO;

namespace SharpAviReader.Riff;

internal abstract class RiffListReaderBase : RiffReaderBase
{
    protected RiffListReaderBase(BinaryReader binaryReader, FourCC chunkId, long bodyLength, RiffListReaderBase? parent = null)
        : base(binaryReader, chunkId, bodyLength, parent)
    {}

    public bool IsEndOfList => CurrentLocalPosition >= ContentLength;

    public RiffChunkReader OpenSubChunk(FourCC expectedChunkId = default)
    {
        if (CheckReadingScope && CurrentLocalPosition + FourCC.SIZE + sizeof(ushort) > ContentLength)
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
