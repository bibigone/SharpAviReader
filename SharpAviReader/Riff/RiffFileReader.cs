using System;
using System.IO;
using System.Text;

namespace SharpAviReader.Riff;

internal class RiffFileReader : RiffListReaderBase
{
    public RiffFileReader(Stream stream, bool leaveStreamOpen)
        : base(new BinaryReader(stream, Encoding.ASCII, leaveStreamOpen), KnownFourCCs.None, stream.Length)
    {
        if (stream is null)
            throw new ArgumentNullException(nameof(stream));
        if (!stream.CanRead || !stream.CanSeek)
            throw RiffExceptions.StreamMustBeReadableAndSeekable(nameof(stream));
    }

    public override void Dispose()
    {
        base.Dispose();
        BinaryReader.Dispose();
    }

    public RiffChunkReader OpenChunkAtPosition(long position, FourCC expectedChunkId = default)
    {
        CurrentLocalPosition = position;
        return OpenSubChunk(expectedChunkId);
    }

    public override string ToString()
        => "FILE";
}
