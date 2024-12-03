using System.Runtime.InteropServices;

namespace SharpAviReader.Avi;

/// <summary>Entry of AVI super index. See <see cref="AviSuperIndex"/>.</summary>
internal readonly struct AviSuperIndexEntry
{
    /// <summary>Size of structure in bytes.</summary>
    public static readonly int SIZE = Marshal.SizeOf<AviSuperIndexEntry>();

    /// <summary>The offset, in bytes, from the start of the file to the sub-index that this entry points to.</summary>
    public long ChunkOffset { get; init; }

    /// <summary>The size of the sub-index, in bytes.</summary>
    public int ChunkSize { get; init; }

    /// <summary>The duration of the file that is covered by the sub-index, in stream ticks.</summary>
    public int Duration { get; init; }

    public override string ToString()
        => $"{{{nameof(ChunkOffset)} = {ChunkOffset}, {nameof(ChunkSize)} = {ChunkSize}, {nameof(Duration)} = {Duration}}}";
}
