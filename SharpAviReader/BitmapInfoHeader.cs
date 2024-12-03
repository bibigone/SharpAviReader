using System;

namespace SharpAviReader;

/// <summary>
/// This record contains information about the dimensions and color format of a device-independent bitmap (DIB).
/// </summary>
/// <remarks>
/// Native: <c>BITMAPINFOHEADER</c> struct.
/// </remarks>
public record BitmapInfoHeader
{
    /// <summary>Specifies the number of bytes required by the structure.</summary>
    /// <remarks>
    /// This value does not include the size of the color table or the size of the color masks,
    /// if they are appended to the end of structure.
    /// </remarks>
    public int Size { get; init; }

    /// <summary>Specifies the width of the bitmap, in pixels.</summary>
    /// <remarks>
    /// Calculation of the stride:
    /// <code>
    /// Stride = ((((Width * BitsPerPixel) + 31) &amp; ~31) >> 3);
    /// ImageSizeInBytes = abs(Height) * Stride;
    /// </code>
    /// </remarks>
    public int Width { get; init; }

    /// <summary>Specifies the height of the bitmap, in pixels.</summary>
    /// <remarks><para>
    /// For uncompressed RGB bitmaps, if <see cref="Height"/> is positive, the bitmap is a bottom-up DIB with the origin at the lower left corner.
    /// If <see cref="Height"/> is negative, the bitmap is a top-down DIB with the origin at the upper left corner.
    /// </para><para>
    /// For YUV bitmaps, the bitmap is always top-down, regardless of the sign of <see cref="Height"/>.
    /// Decoders should offer YUV formats with positive <see cref="Height"/>,
    /// but for backward compatibility they should accept YUV formats with either positive or negative <see cref="Height"/>.
    /// </para><para>
    /// For compressed formats, <see cref="Height"/> must be positive, regardless of image orientation.
    /// </para></remarks>
    public int Height { get; init; }

    /// <summary>Specifies the number of planes for the target device. This value must be set to 1.</summary>
    public short Planes { get; init; }

    /// <summary>Specifies the number of bits per pixel (bpp).</summary>
    /// <remarks>
    /// For uncompressed formats, this value is the average number of bits per pixel.
    /// For compressed formats, this value is the implied bit depth of the uncompressed image,
    /// after the image has been decoded.
    /// </remarks>
    public short BitsPerPixel { get; init; }

    /// <summary>For compressed video and YUV formats, this member is a FOURCC code.</summary>
    /// <seealso cref="VideoCodecIds"/>
    public FourCC Compression { get; init; }

    /// <summary>Specifies the size, in bytes, of the image. This can be set to 0 for uncompressed RGB bitmaps.</summary>
    public int ImageSizeInBytes { get; init; }

    /// <summary>Specifies the horizontal resolution, in pixels per meter, of the target device for the bitmap.</summary>
    public int PixelsPerMeterX { get; init; }

    /// <summary>Specifies the vertical resolution, in pixels per meter, of the target device for the bitmap.</summary>
    public int PixelsPerMeterY { get; init; }

    /// <summary>
    /// Specifies the number of color indices in the color table that are actually used by the bitmap.
    /// </summary>
    /// <seealso cref="BitmapPaletteItem"/>
    public int ColorsUsed { get; init; }

    /// <summary>
    /// Specifies the number of color indices that are considered important for displaying the bitmap.
    /// If this value is zero, all colors are important.
    /// </summary>
    public int ColorsImportant { get; init; }

    /// <summary>Palette items.</summary>
    public BitmapPaletteItem[] Palette {  get; init; } = Array.Empty<BitmapPaletteItem>();
}
