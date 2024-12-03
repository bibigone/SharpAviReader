using System;
using System.IO;
using System.Text;

namespace SharpAviReader.Riff;

internal class RiffChunkReader : RiffReaderBase
{
    public RiffChunkReader(FourCC chunkId, long bodyLength, RiffListReaderBase parent)
        : base(parent.BinaryReader, chunkId, bodyLength, parent)
    { }

    public bool IsList => ContentLength >= FourCC.SIZE && ChunkId.IsList();

    public RiffListReader AsList(FourCC expectedListType = default)
        => RiffListReader.FromChunk(this, expectedListType);

    public FourCC ReadFourCC()
    => new(CheckedReadingCall(BinaryReader.ReadUInt32));

    public byte ReadByte() => CheckedReadingCall(BinaryReader.ReadByte);

    public ushort ReadUInt16() => CheckedReadingCall(BinaryReader.ReadUInt16);

    public short ReadInt16() => CheckedReadingCall(BinaryReader.ReadInt16);

    public uint ReadUInt32() => CheckedReadingCall(BinaryReader.ReadUInt32);

    public int ReadInt32() => CheckedReadingCall(BinaryReader.ReadInt32);

    public long ReadInt64() => CheckedReadingCall(BinaryReader.ReadInt64);

    public byte[] ReadBytes(int count)
    {
        if (CurrentLocalPosition + count > ContentLength)
            throw RiffExceptions.OutOfChunkBoundaries(this);
        return BinaryReader.ReadBytes(count);
    }

    public string ReadAsciiString(int maxLength)
    {
        Span<byte> buffer = stackalloc byte[maxLength];
        var len = 0;
        while (len < buffer.Length)
        {
            var c = CheckedReadingCall(BinaryReader.ReadByte);
            if (c == 0)
                break;
            buffer[len++] = c;
        }
        return len > 0 ? Encoding.ASCII.GetString(buffer[..len]) : string.Empty;
    }
}
