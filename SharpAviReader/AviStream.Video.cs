using SharpAviReader.Avi;
using System;
using System.Collections.Generic;

namespace SharpAviReader;

partial class AviStream
{
    /// <summary>Information about video stream.</summary>
    public class Video : AviStream
    {
        /// <summary>Information about the dimensions and color format of a device-independent bitmap (DIB).</summary>
        public BitmapInfoHeader BitmapInfo { get; }

        /// <summary>FOURCC of video codec.</summary>
        /// <seealso cref="VideoCodecIds"/>
        public FourCC VideoCodecId => header.CodecId;

        /// <summary>Frame rate of video stream.</summary>
        public double FrameRate => Math.Round((double)header.Rate / header.Scale, 3);

        /// <summary>Frame count in video stream.</summary>
        public int FrameCount => header.Length;

        /// <summary>
        /// Specifies the destination rectangle for the video stream within the movie rectangle
        /// specified by the <see cref="AviReader.VideoWidth"/> and <see cref="AviReader.VideoHeight"/>.
        /// </summary>
        public RectInt16 TargetRect => header.Frame;

        internal Video(AviStreamHeader header, byte[]? codecSpecificData, AviSuperIndex? superIndex, BitmapInfoHeader bitmapInfo)
            : base(header, codecSpecificData, superIndex)
        {
            BitmapInfo = bitmapInfo;
        }

        /// <summary>Index treated as correct when it contains the same count of items as <see cref="FrameCount"/>.</summary>
        public override bool? HasCorrectIndexData => FrameCount == Index.Count;

        internal override FourCC? GetDataChunkId(int streamNum)
            => KnownFourCCs.Chunks.VideoFrame(streamNum, compressed: VideoCodecIds.IsVideoCompressed(VideoCodecId));
    }
}
