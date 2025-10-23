using System.Runtime.InteropServices;

namespace SharpAviReader.Avi;

/// <summary>Contains one index entry for an AVI 1.0 index ('idx1' format).</summary>
internal readonly struct AviOldIndexEntry
{
    /// <summary>Size of structure in bytes.</summary>
    public static readonly int SIZE = Marshal.SizeOf<AviOldIndexEntry>();

    /// <summary>Specifies a FOURCC that identifies a stream in the AVI file.</summary>
    /// <remarks>
    /// The FOURCC must have the form <c>xxyy</c> where <c>xx</c> is the stream number and <c>yy</c> is a two-character code that identifies the contents of the stream:
    /// db - uncompressed video frame, dc - compressed video frame, pc - palette change, wb - audio data.
    /// </remarks>
    public FourCC ChunkId { get; init; }

    /// <summary>Specifies a bitwise combination of zero or more of the following flags.</summary>
    /// <seealso cref="AviOldIndexEntryFlags"/>
    public uint Flags { get; init; }

    /// <summary>The data chunk is a 'rec ' list.</summary>
    /// <seealso cref="AviOldIndexEntryFlags.List"/>
    public bool IsRecList => (Flags & AviOldIndexEntryFlags.List) != 0;

    /// <summary>The data chunk does not affect the timing of the stream.</summary>
    /// <remarks>For example, this flag should be set for palette changes.</remarks>
    /// <seealso cref="AviOldIndexEntryFlags.NoTime"/>
    public bool IsNoTime => (Flags & AviOldIndexEntryFlags.NoTime) != 0;

    /// <summary>The data chunk is a key frame.</summary>
    /// <seealso cref="AviOldIndexEntryFlags.KeyFrame"/>
    public readonly bool IsKeyFrame => (Flags & AviOldIndexEntryFlags.KeyFrame) != 0;

    /// <summary>Specifies the location of the data chunk in the file.</summary>
    /// <remarks>
    /// The value should be specified as an offset, in bytes, from the start of the 'movi' list;
    /// however, in some AVI files it is given as an offset from the start of the file.
    /// </remarks>
    public uint Offset { get; init; }

    /// <summary>Specifies the size of the data chunk, in bytes.</summary>
    public uint Size { get; init; }

    public override string ToString()
        => $"{{{nameof(ChunkId)} = {ChunkId}, {nameof(Flags)} = {Flags}, {nameof(Offset)} = {Offset}, {nameof(Size)} = {Size}}}";
}
