namespace SharpAviReader;

/// <summary>Some of possible formats of audio data.</summary>
/// <remarks>For more information visit <see href="https://www.opennet.ru/docs/formats/wavecomp.html"/>.</remarks>
/// <seealso cref="WaveFormatEx.FormatTag"/>
public enum WaveFormatTag : ushort
{
    /// <summary>Unknown format.</summary>
    UNKNOWN = (ushort)0x0000u,

    /// <summary>PCM (pulse-code modulated) data in integer format.</summary>
    PCM = (ushort)0x0001u,

    /// <summary>ADPCM (adaptive differential pulse-code modulated) data.</summary>
    ADPCM = (ushort)0x0002u,

    /// <summary>PCM data in IEEE floating-point format.</summary>
    IEEE_FLOAT = (ushort)0x0003u,

    /// <summary>A-law-encoded format.</summary>
    ALAW = (ushort)0x0006u,

    /// <summary>Mu-law-encoded format.</summary>
    MULAW = (ushort)0x0007u,

    /// <summary>MPEG-1 data format (stream conforms to ISO 11172-3 Audio specification).</summary>
    MPEG = (ushort)0x0050u,

    /// <summary>AC-3 (aka Dolby Digital) over S/PDIF.</summary>
    DOLBY_AC3_SPDIF = (ushort)0x0092u,

    /// <summary>Extensible WAVEFORMATEX structure</summary>
    EXTENSIBLE = (ushort)0xFFFEu,
}
