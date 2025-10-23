namespace SharpAviReader.Avi;

/// <summary>Known flag values for <see cref="AviOldIndexEntry.Flags"/> field of the <see cref="AviOldIndexEntry"/>.</summary>
internal static class AviOldIndexEntryFlags
{
    /// <summary>The data chunk is a key frame</summary>
    public const uint KeyFrame = 0x00000010;

    /// <summary>The data chunk is a 'rec ' list.</summary>
    public const uint List = 0x00000001;

    /// <summary>The data chunk does not affect the timing of the stream. For example, this flag should be set for palette changes.</summary>
    public const uint NoTime = 0x00000100;
}
