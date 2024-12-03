using System.IO;

namespace SharpAviReader.Riff;

internal class RiffListReader : RiffListReaderBase
{
    public static RiffListReader FromChunk(RiffChunkReader chunk, FourCC expectedListType = default)
    {
        var listType = chunk.ReadFourCC();
        if (expectedListType != KnownFourCCs.None && expectedListType != listType)
            throw RiffExceptions.UnexpectedListType(chunk, expectedListType, listType);
        var bodyLength = chunk.ContentLength - FourCC.SIZE;
        return new(chunk.BinaryReader, chunk.ChunkId, listType, bodyLength, chunk.Parent) {CheckReadingScope = chunk.CheckReadingScope };
    }

    private RiffListReader(BinaryReader binaryReader, FourCC chunkId, FourCC listType, long bodyLength, RiffListReaderBase? parent)
        : base(binaryReader, chunkId, bodyLength, parent)
        => ListType = listType;

    public FourCC ListType { get; }

    public override string ToString()
        => $"{ChunkId}:{ListType}";
}
