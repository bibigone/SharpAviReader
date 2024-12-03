using System.Runtime.InteropServices;

namespace SharpAviReader.Avi;

/// <summary>Contains one index entry for an AVI 2.0 standard index. This structure is contained in the <see cref="AviStdIndex"/>.</summary>
internal readonly struct AviStdIndexEntry
{
    /// <summary>Size of structure in bytes.</summary>
    public static readonly int SIZE = Marshal.SizeOf<AviStdIndexEntry>();

    /// <summary>The offset, in bytes, to the start of the data.</summary>
    /// <remarks>
    /// The offset is relative to the value of the <see cref="AviStdIndex.BaseOffset"/> member of the <see cref="AviStdIndex"/>.
    /// The value is the offset of the actual audio/video data in the chunk, not the offset of the start of the chunk.
    /// </remarks>
    public uint Offset { get; init; }

    /// <summary>The lower 31 bits contain the size of the data. The high bit is set to 1 if the frame is delta frame, or zero otherwise.</summary>
    public uint SizePlusDeltaFrameFlag { get; init; }

    public readonly bool IsDeltaFrame => (SizePlusDeltaFrameFlag & 0x80000000u) != 0u;

    public int DataSize => (int)(SizePlusDeltaFrameFlag & 0x7FFFFFFFu);

    public override string ToString()
        => $"{{{nameof(Offset)} = {Offset}, {nameof(SizePlusDeltaFrameFlag)} = {SizePlusDeltaFrameFlag}}}";
}
