using System;

namespace SharpAviReader.Avi;

/// <summary>
/// Standard flags for <see cref="AviMainHeader"/>.
/// </summary>
[Flags]
internal enum AviMainHeaderFlags : uint
{
    /// <summary>Indicates the AVI file has an index.</summary>
    HasIndex = 0x00000010U,

    /// <summary>
    /// Indicates that application should use the index, rather than the physical ordering of the chunks in the file,
    /// to determine the order of presentation of the data.
    /// </summary>
    /// <remarks>
    /// For example, this flag could be used to create a list of frames for editing.
    /// </remarks>
    MustUseIndex = 0x00000020U,

    /// <summary>Indicates the AVI file is interleaved.</summary>
    IsInterleaved = 0x00000100U,

    /// <summary>AVIF_TRUSTCKTYPE</summary>
    TrustChunkType = 0x00000800U,

    /// <summary>Indicates the AVI file is a specially allocated file used for capturing real-time video.</summary>
    /// <remarks>
    /// Applications should warn the user before writing over a file with this flag set
    /// because the user probably defragmented this file.
    /// </remarks>
    WasCaptureFile = 0x00010000U,

    /// <summary>Indicates the AVI file contains copyrighted data and software.</summary>
    /// <remarks>
    /// When this flag is used, software should not permit the data to be duplicated.
    /// </remarks>
    Copyrighted = 0x000200000U,
}
