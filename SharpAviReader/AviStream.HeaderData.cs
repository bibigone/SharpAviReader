using SharpAviReader.Avi;
using SharpAviReader.Riff;

namespace SharpAviReader;

partial class AviStream
{
    /// <summary>Helper class that helps to collect data about a stream.</summary>
    private sealed class HeaderData
    {
        public AviStreamHeader? Header { get; private set; }
        public BitmapInfoHeader? BitmapInfo { get; private set; }
        public WaveFormatEx? WaveFormat { get; private set; }
        public byte[]? CodecSpecificData { get; private set; }
        public string? StreamName { get; private set; }
        public AviSuperIndex? SuperIndex { get; private set; }

        public HeaderData(RiffListReader streamList)
        {
            while (!streamList.IsEndOfList)
            {
                using (var subChunk = streamList.OpenSubChunk())
                {
                    ProcessChunk(subChunk);
                }
            }
        }

        private void ProcessChunk(RiffChunkReader chunk)
        {
            if (chunk.ChunkId == KnownFourCCs.Chunks.StreamHeader)
                ReadHeader(chunk);
            else if (chunk.ChunkId == KnownFourCCs.Chunks.StreamFormat)
                ReadFormat(chunk);
            else if (chunk.ChunkId == KnownFourCCs.Chunks.StreamName)
                ReadStreamName(chunk);
            else if (chunk.ChunkId == KnownFourCCs.Chunks.StreamHeaderAdditionalData)
                ReadCodecSpecificData(chunk);
            else if (chunk.ChunkId == KnownFourCCs.Chunks.StreamIndex)
                ReadSuperIndex(chunk);
        }

        private void ReadHeader(RiffChunkReader chunk)
            => Header = chunk.ReadAviStreamHeader();

        private void ReadFormat(RiffChunkReader chunk)
        {
            if (Header is null)
                return;
            if (Header.IsAudioStream)
                WaveFormat = chunk.ReadWaveFormatEx();
            if (Header.IsVideoStream)
                BitmapInfo = chunk.ReadBitmapInfoHeader();
        }

        private void ReadStreamName(RiffChunkReader chunk)
            => StreamName = chunk.ReadAsciiString((int)chunk.ContentLength);

        private void ReadCodecSpecificData(RiffChunkReader chunk)
            => CodecSpecificData = chunk.ReadBytes((int)chunk.ContentLength);

        private void ReadSuperIndex(RiffChunkReader chunk)
            => SuperIndex = chunk.ReadSuperIndex();
    }
}
