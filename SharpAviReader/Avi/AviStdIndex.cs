using System;

namespace SharpAviReader.Avi;

/// <summary>Contains an AVI 2.0 standard index.</summary>
/// <remarks>
/// Native: <c>AVISTDINDEX</c> struct.
/// </remarks>
internal record AviStdIndex
{
    /// <summary>The size of each index entry, in 4-byte units. The value must be 2.</summary>
    public short FourBytesPerEntry { get; init; }

    /// <summary>The index subtype. The value must be <see cref="AviIndexSubType.Default"/>.</summary>
    public AviIndexSubType IndexSubType { get; init; }

    /// <summary>The index type. The value must be <see cref="AviIndexType.IndexOfChunks"/>.</summary>
    public AviIndexType IndexType { get; init; }

    /// <summary>The number of valid entries in the <see cref="Entries"/> array.</summary>
    public int EntriesInUse { get; init; }

    /// <summary>A FOURCC that identifies the object that is indexed.</summary>
    public FourCC ChunkId { get; init; }

    /// <summary>The base offset for the index entries.</summary>
    /// <remarks>
    /// For each index entry, <see cref="BaseOffset"/> + <see cref="AviStdIndexEntry.Offset"/>
    /// gives the offset from the start of the file to the data.
    /// </remarks>
    public long BaseOffset { get; init; }

    /// <summary>Reserved. Set to zero.</summary>
    public uint Reserved2 { get; init; }

    /// <summary>Index entries themselves.</summary>
    public AviStdIndexEntry[] Entries { get; init; } = Array.Empty<AviStdIndexEntry>();
}
