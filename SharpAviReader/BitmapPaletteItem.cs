namespace SharpAviReader;

/// <summary>Palette item in bitmap information.</summary>
/// <remarks>Native: <c>RGBQUAD</c>.</remarks>
/// <seealso cref="BitmapInfoHeader"/>
public readonly struct BitmapPaletteItem
{
    /// <summary>Blue component.</summary>
    public byte Blue { get; init; }

    /// <summary>Green component.</summary>
    public byte Green { get; init; }

    /// <summary>Red component.</summary>
    public byte Red { get; init; }

    /// <summary>Unused.</summary>
    public byte Reserved { get; init; }

    /// <inheritdoc/>
    public override string ToString()
        => $"{{R:{Red}, G:{Green}, B:{Blue}}}";
}
