using System;

namespace SharpAviReader.Riff;

internal static class RiffExceptions
{
    public static RiffException InconsistentChunkLength(RiffReaderBase chunk, long expectedLength, long actualLength)
        => new(
            $"Inconsistent length of the `{chunk}` chunk: length in header is {expectedLength} but was read {actualLength} bytes.",
            chunk.BinaryReader.BaseStream.Position);

    public static ArgumentOutOfRangeException ArgumentOutOfRange(string paramName, long minValue, long maxValue, long value)
        => new(
            paramName,
            $"Invalid value {value} of the `{paramName}`: value cannot be less than {minValue} and cannot be greater than {maxValue}.");

    public static RiffException OutOfChunkBoundaries(RiffReaderBase chunk)
        => new(
            $"Attempt to read out of the `{chunk}` chunk boundaries.",
            chunk.BinaryReader.BaseStream.Position);

    public static ArgumentException StreamMustBeReadableAndSeekable(string paramName)
        => new(
            "Cannot read AVI data from the stream specified: stream should allow to read and to seek.",
            paramName);

    public static RiffException UnexpectedListType(RiffReaderBase chunk, FourCC expectedListType, FourCC actualListType)
        => new(
            $"Unexpected type of the list `{chunk.ChunkId}`: expected `{expectedListType}` but actual is {actualListType}.",
            chunk.BinaryReader.BaseStream.Position);

    public static RiffException EndOfList(RiffListReaderBase list)
        => new(
            $"End of list `{list}`.",
            list.BinaryReader.BaseStream.Position);

    public static RiffException UnexpectedChunkId(RiffListReaderBase list, FourCC expectedChunkId, FourCC actualChunkId)
        => new(
            $"Unexpected chunk ID during reading of the `{list}`: expected `{expectedChunkId}` but actual is {actualChunkId}.",
            list.BinaryReader.BaseStream.Position);

    public static RiffException InvalidSizeOfSuperIndexEntry(RiffChunkReader chunk, int expectedSize, int actualSize)
        => new(
            $"Invalid size of super index entry (`{chunk}` chunk): expected {expectedSize} bytes but actual is {actualSize} bytes.",
            chunk.BinaryReader.BaseStream.Position);

    public static RiffException InvalidSizeOfStdIndexEntry(RiffChunkReader chunk, int expectedSize, int actualSize)
        => new(
            $"Invalid size of STD index entry (`{chunk}` chunk): expected {expectedSize} bytes but actual is {actualSize} bytes.",
            chunk.BinaryReader.BaseStream.Position);

    public static RiffException StreamHeaderNotFound(RiffListReader list)
        => new(
            $"Stream header not found in the list `{list}`.",
            list.BinaryReader.BaseStream.Position);

    public static RiffException VideoFormatNotFound(RiffListReader list)
        => new(
            $"Video format chunk is not found in the list `{list}` for video stream.",
            list.BinaryReader.BaseStream.Position);

    public static RiffException SuperIndexIsNotFoundForVideoStream(RiffListReader list)
        => new(
            $"Super index chunk is not found in the list `{list}` for video stream.",
            list.BinaryReader.BaseStream.Position);

    public static RiffException WaveFormatNotFound(RiffListReader list)
        => new(
            $"Wave format chunk is not found in the list `{list}` for audio stream.",
            list.BinaryReader.BaseStream.Position);
}
