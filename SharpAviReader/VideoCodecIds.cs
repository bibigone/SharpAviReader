namespace SharpAviReader;

/// <summary>Identifiers of various video codecs.</summary>
public static class VideoCodecIds
{
    /// <summary>Identifier used for non-compressed data.</summary>
    public static readonly FourCC Uncompressed = new(0);

    /// <summary>
    /// The bitmap is not compressed, and the color table consists of three DWORD (defined in [MS-DTYP] section 2.2.9)
    /// color masks that specify the red, green, and blue components, respectively, of each pixel.
    /// This is valid when used with 16 and 32-bits per pixel bitmaps.
    /// </summary>
    public static readonly FourCC BitFields = new(3);

    /// <summary>Motion JPEG.</summary>
    public static readonly FourCC MotionJpeg = new("MJPG");

    /// <summary>Microsoft MPEG-4 V3.</summary>
    public static readonly FourCC MicrosoftMpeg4V3 = new("MP43");

    /// <summary>Microsoft MPEG-4 V2.</summary>
    public static readonly FourCC MicrosoftMpeg4V2 = new("MP42");

    /// <summary>Xvid MPEG-4.</summary>
    public static readonly FourCC Xvid = new("XVID");

    /// <summary>DivX MPEG-4.</summary>
    public static readonly FourCC DivX = new("DIVX");

    /// <summary>x264 H.264/MPEG-4 AVC.</summary>
    public static readonly FourCC X264 = new("X264");
}
