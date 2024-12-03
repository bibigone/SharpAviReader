using System;

namespace SharpAviReader.Avi;

/// <summary>
/// Standard flags for <see cref="AviStreamHeader"/>.
/// </summary>
[Flags]
internal enum AviStreamHeaderFlags : uint
{
    /// <summary>Indicates this stream should not be enabled by default.</summary>
    /// <remarks>Native: <c>AVISF_DISABLED</c>.</remarks>
    Disabled = 0x00000001u,

    /// <summary>
    /// Indicates this video stream contains palette changes.
    /// This flag warns the playback software that it will need to animate the palette.
    /// </summary>
    /// <remarks>Native: <c>AVISF_VIDEO_PALCHANGES</c>.</remarks>
    VideoPalChanges = 0x00010000U,
}
