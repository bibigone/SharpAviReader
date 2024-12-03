using System.IO;
using SharpAviReader.Avi;

namespace SharpAviReader.Riff;

/// <summary>Extension methods that help to parse RIFF files.</summary>
internal static class RiffReadingExtensions
{
    public static FourCC ReadFourCC(this BinaryReader reader)
        => new(reader.ReadUInt32());

    public static AviMainHeader ReadAviHeader(this RiffChunkReader reader)
        => new()
        {
            MicrosecondsPerFrame = reader.ReadInt32(),
            MaxBytesPerSecond = reader.ReadInt32(),
            PaddingGranularity = reader.ReadInt32(),
            Flags = (AviMainHeaderFlags)reader.ReadUInt32(),
            TotalFrames = reader.ReadInt32(),
            InitialFrames = reader.ReadInt32(),
            StreamCount = reader.ReadInt32(),
            SuggestedBufferSize = reader.ReadInt32(),
            VideoWidth = reader.ReadInt32(),
            VideoHeight = reader.ReadInt32(),
            Reserved = reader.ReadUInt32(),
        };

    public static RectInt16 ReadRectInt16(this RiffChunkReader reader)
        => new()
        {
            Left = reader.ReadInt16(),
            Top = reader.ReadInt16(),
            Right = reader.ReadInt16(),
            Bottom = reader.ReadInt16(),
        };

    public static AviStreamHeader ReadAviStreamHeader(this RiffChunkReader reader)
        => new()
        {
            StreamType = reader.ReadFourCC(),
            CodecId = reader.ReadFourCC(),
            Flags = (AviStreamHeaderFlags)reader.ReadUInt32(),
            Priority = reader.ReadUInt16(),
            Language = reader.ReadUInt16(),
            InitialFrames = reader.ReadInt32(),
            Scale = reader.ReadInt32(),
            Rate = reader.ReadInt32(),
            Start = reader.ReadInt32(),
            Length = reader.ReadInt32(),
            SuggestedBufferSize = reader.ReadInt32(),
            Quality = reader.ReadInt32(),
            SampleSize = reader.ReadInt32(),
            Frame = reader.ReadRectInt16(),
        };

    public static BitmapInfoHeader ReadBitmapInfoHeader(this RiffChunkReader reader)
    {
        var res = new BitmapInfoHeader()
        {
            Size = reader.ReadInt32(),
            Width = reader.ReadInt32(),
            Height = reader.ReadInt32(),
            Planes = reader.ReadInt16(),
            BitsPerPixel = reader.ReadInt16(),
            Compression = reader.ReadFourCC(),
            ImageSizeInBytes = reader.ReadInt32(),
            PixelsPerMeterX = reader.ReadInt32(),
            PixelsPerMeterY = reader.ReadInt32(),
            ColorsUsed = reader.ReadInt32(),
            ColorsImportant = reader.ReadInt32(),
        };

        if (res.ColorsUsed >= 0)
            res = res with { Palette = reader.ReadBitmapPaletteItems(res.ColorsUsed) };

        return res;
    }

    public static BitmapPaletteItem ReadBitmapPaletteItem(this RiffChunkReader reader)
        => new()
        {
            Blue = reader.ReadByte(),
            Green = reader.ReadByte(),
            Red = reader.ReadByte(),
            Reserved = reader.ReadByte(),
        };

    public static BitmapPaletteItem[] ReadBitmapPaletteItems(this RiffChunkReader reader, int count)
    {
        var res = new BitmapPaletteItem[count];
        for (int i = 0; i < count; i++)
            res[i] = reader.ReadBitmapPaletteItem();
        return res;
    }

    public static WaveFormatEx ReadWaveFormatEx(this RiffChunkReader reader)
        => new()
        {
            FormatTag = (WaveFormatTag)reader.ReadUInt16(),
            Channels = reader.ReadInt16(),
            SamplesPerSec = reader.ReadInt32(),
            AvgBytesPerSec = reader.ReadInt32(),
            BlockAlign = reader.ReadInt16(),
            BitsPerSample = reader.ReadInt16(),
            ExtraDataSize = reader.ReadInt16(),
        };

    public static AviSuperIndex ReadSuperIndex(this RiffChunkReader reader)
    {
        var res = new AviSuperIndex
        {
            FourBytesPerEntry = reader.ReadInt16(),
            IndexSubType = (AviIndexSubType)reader.ReadByte(),
            IndexType = (AviIndexType)reader.ReadByte(),
            EntriesInUse = reader.ReadInt32(),
            ChunkId = reader.ReadFourCC(),
            Reserved0 = reader.ReadUInt32(),
            Reserved1 = reader.ReadUInt32(),
            Reserved2 = reader.ReadUInt32(),
        };
        if (res.EntriesInUse > 0)
        {
            if (res.FourBytesPerEntry * 4 != AviSuperIndexEntry.SIZE)
                throw RiffExceptions.InvalidSizeOfSuperIndexEntry(reader, AviSuperIndexEntry.SIZE, res.FourBytesPerEntry * 4);
            res = res with { Entries = reader.ReadSuperIndexEntries(res.EntriesInUse) };
        }
        return res;
    }

    public static AviSuperIndexEntry ReadSuperIndexEntry(this RiffChunkReader reader)
        => new()
        {
            ChunkOffset = reader.ReadInt64(),
            ChunkSize = reader.ReadInt32(),
            Duration = reader.ReadInt32(),
        };

    public static AviSuperIndexEntry[] ReadSuperIndexEntries(this RiffChunkReader reader, int count)
    {
        var res = new AviSuperIndexEntry[count];
        for (var i = 0; i < count; i++)
            res[i] = reader.ReadSuperIndexEntry();
        return res;
    }

    public static AviStdIndex ReadStdIndex(this RiffChunkReader reader)
    {
        var res = new AviStdIndex
        {
            FourBytesPerEntry = reader.ReadInt16(),
            IndexSubType = (AviIndexSubType)reader.ReadByte(),
            IndexType = (AviIndexType)reader.ReadByte(),
            EntriesInUse = reader.ReadInt32(),
            ChunkId = reader.ReadFourCC(),
            BaseOffset = reader.ReadInt64(),
            Reserved2 = reader.ReadUInt32(),
        };
        if (res.EntriesInUse > 0)
        {
            if (res.FourBytesPerEntry * 4 != AviStdIndexEntry.SIZE)
                throw RiffExceptions.InvalidSizeOfSuperIndexEntry(reader, AviStdIndexEntry.SIZE, res.FourBytesPerEntry * 4);
            res = res with { Entries = reader.ReadStdIndexEntries(res.EntriesInUse) };
        }
        return res;
    }

    public static AviStdIndexEntry ReadStdIndexEntry(this RiffChunkReader reader)
        => new()
        {
            Offset = reader.ReadUInt32(),
            SizePlusDeltaFrameFlag = reader.ReadUInt32(),
        };

    public static AviStdIndexEntry[] ReadStdIndexEntries(this RiffChunkReader reader, int count)
    {
        var res = new AviStdIndexEntry[count];
        for (var i = 0; i < count; i++)
            res[i] = reader.ReadStdIndexEntry();
        return res;
    }
}
