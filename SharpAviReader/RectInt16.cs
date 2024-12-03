namespace SharpAviReader;

/// <summary>
/// Rectangle that is specified by four <see cref="short"/> numbers.
/// </summary>
public readonly struct RectInt16
{
    /// <summary>X coordinate of the left-top corner.</summary>
    public short Left { get; init; }

    /// <summary>Y coordinate of the left-top corner.</summary>
    public short Top { get; init; }

    /// <summary>X coordinate of the right-bottom corner.</summary>
    public short Right { get; init; }

    /// <summary>Y coordinate of the right-bottom corner.</summary>
    public short Bottom { get; init; }

    /// <summary>Width (distance between right and left edges).</summary>
    public readonly int Width => Right - Left;

    /// <summary>Height (distance between bottom and top edges).</summary>
    public readonly int Height => Bottom - Top;

    /// <inheritdoc />
    public override string ToString()
        => $"{nameof(RectInt16)} ({Left},{Top})-({Right},{Bottom})";
}
