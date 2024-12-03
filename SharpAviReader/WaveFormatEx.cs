namespace SharpAviReader;

/// <summary>This record defines the format of waveform-audio data.</summary>
/// <remarks><para>
/// Only format information common to all waveform-audio data formats is included in this structure.
/// For formats that require additional information, this structure is included as the first member in another structure,
/// along with the additional information.
/// </para><para>
/// Native: <c>WAVEFORMATEX</c> struct.
/// </para></remarks>
public record WaveFormatEx
{
    /// <summary>Waveform-audio format type.</summary>
    /// <remarks>
    /// Format tags are registered with Microsoft Corporation for many compression algorithms.
    /// A complete list of format tags can be found in the Mmreg.h header file.
    /// For one- or two-channel PCM data, this value should be <see cref="WaveFormatTag.PCM"/>.
    /// </remarks>
    public WaveFormatTag FormatTag { get; init; }

    /// <summary>Number of channels in the waveform-audio data.</summary>
    /// <remarks>
    /// Monaural data uses one channel and stereo data uses two channels.
    /// </remarks>
    public short Channels { get; init; }

    /// <summary>Sample rate, in samples per second (hertz).</summary>
    /// <remarks>
    /// If <see cref="FormatTag"/> is <see cref="WaveFormatTag.PCM"/>, then common values for <see cref="SamplesPerSec"/> are 8.0 kHz, 11.025 kHz, 22.05 kHz, and 44.1 kHz.
    /// For non-PCM formats, this member must be computed according to the manufacturer's specification of the format tag.
    /// </remarks>
    public int SamplesPerSec { get; init; }

    /// <summary>Required average data-transfer rate, in bytes per second, for the format tag.</summary>
    /// <remarks>
    /// If <see cref="FormatTag"/> is <see cref="WaveFormatTag.PCM"/>, <see cref="AvgBytesPerSec"/> should be equal to the product of <see cref="SamplesPerSec"/> and <see cref="BlockAlign"/>.
    /// For non-PCM formats, this member must be computed according to the manufacturer's specification of the format tag.
    /// </remarks>
    public int AvgBytesPerSec { get; init; }

    /// <summary>Block alignment, in bytes.</summary>
    /// <remarks>
    /// The block alignment is the minimum atomic unit of data for the wFormatTag format type.
    /// If <see cref="FormatTag"/> is <see cref="WaveFormatTag.PCM"/> or <see cref="WaveFormatTag.EXTENSIBLE"/>,
    /// <see cref="BlockAlign"/> must be equal to the product of <see cref="Channels"/> and <see cref="BitsPerSample"/> divided by 8 (bits per byte).
    /// For non-PCM formats, this member must be computed according to the manufacturer's specification of the format tag.
    /// </remarks>
    public short BlockAlign { get; init; }

    /// <summary>Bits per sample for the wFormatTag format type.</summary>
    /// <remarks>
    /// If <see cref="FormatTag"/> is <see cref="WaveFormatTag.PCM"/>, then <see cref="BitsPerSample"/> should be equal to 8 or 16.
    /// For non-PCM formats, this member must be set according to the manufacturer's specification of the format tag.
    /// </remarks>
    public short BitsPerSample { get; init; }

    /// <summary>Size, in bytes, of extra format information appended to the end of this structure.</summary>
    /// <remarks>
    /// This information can be used by non-PCM formats to store extra attributes for the <see cref="FormatTag"/>.
    /// If no extra information is required by the <see cref="FormatTag"/>, this member must be set to 0.
    /// </remarks>
    public short ExtraDataSize { get; init; }
}
