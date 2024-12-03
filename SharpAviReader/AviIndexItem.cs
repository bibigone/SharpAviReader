namespace SharpAviReader;

/// <summary>Information about frame data position and size in a file / stream.</summary>
public readonly struct AviIndexItem
{
    /// <summary>Offset in bytes to frame data.</summary>
    public long Offset { get; init; }

    /// <summary>Frame data size in bytes.</summary>
    public int DataSize { get; init; }

    /// <summary>Is frame a delta-frame?</summary>
    /// <remarks><see langword="true"/> - delta frame, <see langword="false"/> - key frame.</remarks>
    public bool IsDeltaFrame { get; init; }
}