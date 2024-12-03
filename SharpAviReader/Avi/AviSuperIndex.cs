using System;

namespace SharpAviReader.Avi;

/// <summary>Contains an AVI 2.0 super index (index of indexes).</summary>
/// <remarks>
/// Native: <c>AVISUPERINDEX</c> struct.
/// </remarks>
internal record AviSuperIndex
{
    /// <summary>The size of each index entry, in 4-byte units. The value must be 4.</summary>
    public short FourBytesPerEntry { get; init; }

    /// <summary>The index subtype. The value must be <see cref="AviIndexSubType.Default"/> or <see cref="AviIndexSubType.ToField"/>.</summary>
    public AviIndexSubType IndexSubType { get; init; }

    /// <summary>The index type. The value must be <see cref="AviIndexType.IndexOfIndexes"/>.</summary>
    public AviIndexType IndexType { get; init; }

    /// <summary>The number of valid entries in the <see cref="Entries"/> array.</summary>
    public int EntriesInUse { get; init; }

    /// <summary>A FOURCC that identifies the object that is indexed.</summary>
    public FourCC ChunkId { get; init; }

    /// <summary>Reserved. Set to zero.</summary>
    public uint Reserved0 { get; init; }

    /// <summary>Reserved. Set to zero.</summary>
    public uint Reserved1 { get; init; }

    /// <summary>Reserved. Set to zero.</summary>
    public uint Reserved2 { get; init; }

    /// <summary>Index entries themselves.</summary>
    public AviSuperIndexEntry[] Entries { get; init; } = Array.Empty<AviSuperIndexEntry>();
}
