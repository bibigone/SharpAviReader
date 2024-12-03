using System;
using System.Runtime.Serialization;

namespace SharpAviReader;

/// <summary>
/// Represents errors that occur during the parsing of RIFF (Resource Interchange File Format) files / streams.
/// </summary>
[Serializable]
public class RiffException : Exception
{
    /// <summary>Creates a new instance of the exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="filePosition">Error position in the RIFF file/stream.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.</param>
    public RiffException(string message, long filePosition, Exception? innerException = null)
        : base(message, innerException)
        => FilePosition = filePosition;

    /// <summary>For binary serialization.</summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected RiffException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        => FilePosition = info.GetInt64(nameof(FilePosition));

    /// <summary>Error position in the RIFF file/stream.</summary>
    public long FilePosition { get; }

    /// <inheritdoc />
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(FilePosition), FilePosition, typeof(long));
    }
}
