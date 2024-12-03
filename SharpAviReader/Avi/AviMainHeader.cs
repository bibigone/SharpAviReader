using System;

namespace SharpAviReader.Avi;

/// <summary>This record defines global information in an AVI file.</summary>
/// <remarks>
/// Native: <c>AVIMAINHEADER</c> struct.
/// </remarks>
internal record AviMainHeader
{
    /// <summary>Specifies the number of microseconds between frames.</summary>
    /// <remarks>
    /// This value indicates the overall timing for the file.
    /// </remarks>
    public int MicrosecondsPerFrame { get; init; }

    /// <summary>Calculated frame rate from <see cref="MicrosecondsPerFrame"/>.</summary>
    public double FramesPerSecond => Math.Round(1000000.0 / MicrosecondsPerFrame, 3);

    /// <summary>Specifies the approximate maximum data rate of the file.</summary>
    /// <remarks>
    /// This value indicates the number of bytes per second the system must handle to present an AVI sequence
    /// as specified by the other parameters contained in the main header and stream header chunks.
    /// </remarks>
    public int MaxBytesPerSecond { get; init; }

    /// <summary>Specifies the alignment for data, in bytes. Pad the data to multiples of this value.</summary>
    public int PaddingGranularity { get; init; }

    /// <summary>Contains a bitwise combination of zero or more of the following flags: <see cref="AviMainHeaderFlags"/>.</summary>
    public AviMainHeaderFlags Flags { get; init; }

    /// <summary>Specifies the total number of frames of data in the file.</summary>
    public int TotalFrames { get; init; }

    /// <summary>Specifies the initial frame for interleaved files.</summary>
    /// <remarks><para>
    /// Noninterleaved files should specify zero.
    /// If you are creating interleaved files, specify the number of frames in the file prior to the initial frame of the AVI sequence in this member.
    /// </para><para>
    /// To give the audio driver enough audio to work with, the audio data in an interleaved file must be skewed from the video data.
    /// Typically, the audio data should be moved forward enough frames to allow approximately 0.75 seconds of audio data to be preloaded.
    /// The <see cref="InitialFrames"/> member should be set to the number of frames the audio is skewed.
    /// Also set the same value for the <see cref="AviStreamHeader.InitialFrames"/> member of the <see cref="AviStreamHeader"/> structure in the audio stream header.
    /// </para></remarks>
    public int InitialFrames { get; init; }

    /// <summary>Specifies the number of streams in the file.</summary>
    /// <remarks>For example, a file with audio and video has two streams.</remarks>
    public int StreamCount { get; init; }

    /// <summary>Specifies the suggested buffer size for reading the file.</summary>
    /// <remarks>
    /// Generally, this size should be large enough to contain the largest chunk in the file.
    /// If set to zero, or if it is too small, the playback software will have to reallocate memory during playback,
    /// which will reduce performance. For an interleaved file,
    /// the buffer size should be large enough to read an entire record, and not just a chunk.
    /// </remarks>
    public int SuggestedBufferSize { get; init; }

    /// <summary>Specifies the width of the AVI file in pixels.</summary>
    public int VideoWidth { get; init; }

    /// <summary>Specifies the height of the AVI file in pixels.</summary>
    public int VideoHeight { get; init; }

    /// <summary>Reserved. Set this array to zero.</summary>
    public uint Reserved { get; init; }
}
