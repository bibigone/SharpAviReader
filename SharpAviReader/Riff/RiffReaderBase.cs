using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SharpAviReader.Riff;

internal abstract class RiffReaderBase : IDisposable
{
    protected readonly long contentStartPosition;

    protected RiffReaderBase(BinaryReader binaryReader, FourCC chunkId, long bodyLength, RiffListReaderBase? parent = null)
    {
        BinaryReader = binaryReader;
        ChunkId = chunkId;
        ContentLength = bodyLength;
        Parent = parent;
        contentStartPosition = binaryReader.BaseStream.Position;

        if (parent is not null)
            CheckReadingScope = parent.CheckReadingScope;
    }

    public virtual void Dispose()
    {
        if (CheckReadingScope && CurrentLocalPosition > ContentLength)
            throw RiffExceptions.InconsistentChunkLength(this, ContentLength, CurrentLocalPosition);
        CurrentLocalPosition = ContentLength;
    }

    public override string ToString()
        => ChunkId.ToString();

    public BinaryReader BinaryReader { get; }

    public FourCC ChunkId { get; }

    public RiffListReaderBase? Parent { get; }

    public bool CheckReadingScope { get; set; }

    public long CurrentLocalPosition
    {
        get => BinaryReader.BaseStream.Position - contentStartPosition;

        protected set
        {
            if (CheckReadingScope && (value < 0 || value > ContentLength))
                throw RiffExceptions.ArgumentOutOfRange(nameof(CurrentLocalPosition), 0, ContentLength, value);
            BinaryReader.BaseStream.Position = value + contentStartPosition;
        }
    }

    public long ContentLength { get; }

    public IDisposable RestorePositionAfter()
        => new PositionRestorer(this);

    private sealed class PositionRestorer : IDisposable
    {
        private readonly RiffReaderBase owner;
        private readonly long position;
        public PositionRestorer(RiffReaderBase owner) => (this.owner, position) = (owner, owner.CurrentLocalPosition);
        public void Dispose() => owner.CurrentLocalPosition = position;
    }

    protected T CheckedReadingCall<T>(Func<T> readingFunc) where T : unmanaged
    {
        if (CheckReadingScope)
        {
            var pos = CurrentLocalPosition;
            if (pos < 0 || pos + Marshal.SizeOf<T>() > ContentLength)
                throw RiffExceptions.OutOfChunkBoundaries(this);
        }

        return readingFunc.Invoke();
    }
}
