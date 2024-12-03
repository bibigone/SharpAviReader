using SharpAviReader.Avi;

namespace SharpAviReader;

partial class AviStream
{
    /// <summary>Information about audio stream.</summary>
    public class Audio : AviStream
    {
        /// <summary>Defines the format of waveform-audio data.</summary>
        public WaveFormatEx WaveFormat { get; }

        /// <summary>Total data size of all frames in bytes.</summary>
        public int TotalDataSize => header.Length;

        /// <summary>Minimum amount of data in bytes.</summary>
        public int Granularity => header.SampleSize;

        internal Audio(AviStreamHeader header, byte[]? codecSpecificData, AviSuperIndex? superIndex, WaveFormatEx waveFormat)
            : base(header, codecSpecificData, superIndex)
        {
            WaveFormat = waveFormat;
        }
    }
}
