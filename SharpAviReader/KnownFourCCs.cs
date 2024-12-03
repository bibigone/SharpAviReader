using System;

namespace SharpAviReader;

/// <summary>
/// Contains definitions of known FOURCC values.
/// </summary>
/// <seealso cref="FourCC"/>
public static class KnownFourCCs
{
    /// <summary>Zero value.</summary>
    public static readonly FourCC None = default;

    /// <summary>Top-level list type.</summary>
    public static readonly FourCC Riff = new("RIFF");

    /// <summary>Non top-level list type.</summary>
    public static readonly FourCC List = new("LIST");

    /// <summary>Determines whether FOURCC is a LIST / RIFF chunk or not.</summary>
    /// <param name="fourCC">Chunk ID.</param>
    /// <returns><see langword="true"/> - chunk is a list of other chunks.</returns>
    public static bool IsList(this FourCC fourCC)
        => fourCC == Riff || fourCC == List;

    /// <summary>
    /// RIFF chunk identifiers used in AVI format.
    /// </summary>
    internal static class Chunks
    {
        /// <summary>Main AVI header.</summary>
        public static readonly FourCC AviHeader = new("avih");

        /// <summary>Stream header.</summary>
        public static readonly FourCC StreamHeader = new("strh");

        /// <summary>Stream format.</summary>
        public static readonly FourCC StreamFormat = new("strf");

        /// <summary>Stream name.</summary>
        public static readonly FourCC StreamName = new("strn");

        /// <summary>Additional (codec-specific) stream header data.</summary>
        public static readonly FourCC StreamHeaderAdditionalData = new("strd");

        /// <summary>Stream index.</summary>
        public static readonly FourCC StreamIndex = new("indx");

        /// <summary>Index v1.</summary>
        public static readonly FourCC Index1 = new("idx1");

        /// <summary>OpenDML header.</summary>
        public static readonly FourCC OpenDmlHeader = new("dmlh");

        /// <summary>Junk chunk.</summary>
        public static readonly FourCC Junk = new("JUNK");

        /// <summary>Gets the identifier of a video frame chunk.</summary>
        /// <param name="streamIndex">Sequential number of the stream.</param>
        /// <param name="compressed">Whether stream contents is compressed.</param>
        public static FourCC VideoFrame(int streamIndex, bool compressed)
        {
            CheckStreamIndex(streamIndex);
            return new(string.Format(compressed ? "{0:00}dc" : "{0:00}db", streamIndex));
        }

        /// <summary>Gets the identifier of an audio data chunk.</summary>
        /// <param name="streamIndex">Sequential number of the stream.</param>
        public static FourCC AudioData(int streamIndex)
        {
            CheckStreamIndex(streamIndex);
            return new(string.Format("{0:00}wb", streamIndex));
        }

        /// <summary>Gets the identifier of an index chunk.</summary>
        /// <param name="streamIndex">Sequential number of the stream.</param>
        public static FourCC IndexData(int streamIndex)
        {
            CheckStreamIndex(streamIndex);
            return new(string.Format("ix{0:00}", streamIndex));
        }

        private static void CheckStreamIndex(int streamIndex)
        {
            if (streamIndex < 0 || streamIndex > 99)
                throw new ArgumentOutOfRangeException(nameof(streamIndex));
        }
    }

    /// <summary>
    /// RIFF list types used in AVI format.
    /// </summary>
    internal static class Lists
    {
        /// <summary>Top-level AVI list.</summary>
        public static readonly FourCC Avi = new("AVI ");

        /// <summary>Top-level extended AVI list.</summary>
        public static readonly FourCC AviExtended = new("AVIX");

        /// <summary>Header list.</summary>
        public static readonly FourCC Header = new("hdrl");

        /// <summary>List containing stream information.</summary>
        public static readonly FourCC Stream = new("strl");

        /// <summary>List containing OpenDML headers.</summary>
        public static readonly FourCC OpenDml = new("odml");

        /// <summary>List with content chunks.</summary>
        public static readonly FourCC Movie = new("movi");
    }

    /// <summary>
    /// Identifiers of the stream types used in AVI format.
    /// </summary>
    internal static class StreamTypes
    {
        /// <summary>Video stream.</summary>
        public static readonly FourCC Video = new("vids");

        /// <summary>Audio stream.</summary>
        public static readonly FourCC Audio = new("auds");
    }

    /// <summary>This is a video stream, isn't it?</summary>
    /// <param name="streamType">Stream type.</param>
    /// <returns><see langword="true"/> when stream type corresponds to a video stream.</returns>
    public static bool IsVideoStream(this FourCC streamType) => streamType == StreamTypes.Video;

    /// <summary>This is an audio stream, isn't it?</summary>
    /// <param name="streamType">Stream type.</param>
    /// <returns><see langword="true"/> when stream type corresponds to an audio stream.</returns>
    public static bool IsAudioStream(this FourCC streamType) => streamType == StreamTypes.Audio;
}
