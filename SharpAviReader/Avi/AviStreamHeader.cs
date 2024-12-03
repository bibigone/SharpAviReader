namespace SharpAviReader.Avi;

/// <summary>This record contains information about one stream in an AVI file.</summary>
/// <remarks><para>
/// Some of the members of this structure are also present in the <see cref="AviMainHeader"/> structure.
/// The data in the <see cref="AviMainHeader"/> structure applies to the whole file,
/// while the data in the <see cref="AviStreamHeader"/> structure applies to one stream.
/// </para><para>
/// Native: <c>AVISTREAMHEADER</c> struct.
/// </para></remarks>
internal record AviStreamHeader
{
    /// <summary>Contains a FOURCC that specifies the type of the data contained in the stream.</summary>
    public FourCC StreamType { get; init; }

    /// <summary>Determines is this a video stream based on <see cref="StreamType"/> field value.</summary>
    /// <seealso cref="KnownFourCCs.IsVideoStream(FourCC)"/>
    public bool IsVideoStream => StreamType.IsVideoStream();

    /// <summary>Determines is this an audio stream based on <see cref="StreamType"/> field value.</summary>
    /// <seealso cref="KnownFourCCs.IsAudioStream(FourCC)"/>
    public bool IsAudioStream => StreamType.IsAudioStream();

    /// <summary>
    /// Optionally, contains a FOURCC that identifies a specific data handler. The data handler is the preferred handler for the stream.
    /// For audio and video streams, this specifies the codec for decoding the stream.
    /// </summary>
    /// <seealso cref="VideoCodecIds"/>
    public FourCC CodecId { get; init; }

    /// <summary>Contains any flags for the data stream.</summary>
    /// <remarks>
    /// The bits in the high-order word of these flags are specific to the type of data contained in the stream.
    /// </remarks>
    public AviStreamHeaderFlags Flags { get; init; }

    /// <summary>Specifies priority of a stream type.</summary>
    /// <remarks>
    /// For example, in a file with multiple audio streams, the one with the highest priority might be the default stream.
    /// </remarks>
    public ushort Priority { get; init; }

    /// <summary>Language tag.</summary>
    public ushort Language { get; init; }

    /// <summary>Specifies how far audio data is skewed ahead of the video frames in interleaved files.</summary>
    /// <remarks>
    /// Typically, this is about 0.75 seconds.
    /// If you are creating interleaved files, specify the number of frames in the file prior to the initial frame
    /// of the AVI sequence in this member. For more information, see the remarks for the <see cref="AviMainHeader.InitialFrames"/> field of <see cref="AviMainHeader"/> struct.
    /// </remarks>
    public int InitialFrames { get; init; }

    /// <summary>Used with <see cref="Rate"/> to specify the time scale that this stream will use.</summary>
    /// <remarks>
    /// Dividing dwRate by dwScale gives the number of samples per second.
    /// For video streams, this is the frame rate.
    /// For audio streams, this rate corresponds to the time needed to play <c>BlockAlign</c> bytes of audio,
    /// which for PCM audio is the just the sample rate.
    /// </remarks>
    public int Scale { get; init; }

    /// <summary><see cref="Scale"/>.</summary>
    public int Rate { get; init; }

    /// <summary>Specifies the starting time for this stream.</summary>
    /// <remarks>
    /// The units are defined by the <see cref="Rate"/> and <see cref="Scale"/> members in the main file header.
    /// Usually, this is zero, but it can specify a delay time for a stream that does not start concurrently with the file.
    /// </remarks>
    public int Start { get; init; }

    /// <summary>Specifies the length of this stream.</summary>
    /// <remarks>
    /// Specifies the length of this stream. The units are defined by the <see cref="Rate"/> and <see cref="Scale"/> members of the stream's header.
    /// </remarks>
    public int Length { get; init; }

    /// <summary>Specifies how large a buffer should be used to read this stream.</summary>
    /// <remarks>
    /// Typically, this contains a value corresponding to the largest chunk present in the stream.
    /// Using the correct buffer size makes playback more efficient.
    /// Use zero if you do not know the correct buffer size.
    /// </remarks>
    public int SuggestedBufferSize { get; init; }

    /// <summary>Specifies an indicator of the quality of the data in the stream.</summary>
    /// <remarks>
    /// Quality is represented as a number between 0 and 10,000.
    /// For compressed data, this typically represents the value of the quality parameter passed to the compression software.
    /// If set to –1, drivers use the default quality value.
    /// </remarks>
    public int Quality { get; init; }

    /// <summary>Specifies the size of a single sample of data.</summary>
    /// <remarks>
    /// This is set to zero if the samples can vary in size.
    /// If this number is nonzero, then multiple samples of data can be grouped into a single chunk within the file.
    /// If it is zero, each sample of data (such as a video frame) must be in a separate chunk.
    /// For video streams, this number is typically zero, although it can be nonzero if all video frames are the same size.
    /// For audio streams, this number should be the same as the <see cref="WaveFormatEx.BlockAlign"/> member of the <see cref="WaveFormatEx"/> structure describing the audio.
    /// </remarks>
    public int SampleSize { get; init; }

    /// <summary>
    /// Specifies the destination rectangle for a text or video stream within the movie rectangle
    /// specified by the <see cref="AviMainHeader.VideoWidth"/> and <see cref="AviMainHeader.VideoHeight"/>
    /// members of the AVI main header structure.
    /// </summary>
    public RectInt16 Frame { get; init; }
}
