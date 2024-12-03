namespace SharpAviReader.Avi;

/// <summary>The sub type of an AVI index.</summary>
internal enum AviIndexSubType : byte
{
    /// <summary>Each index entry is an <see cref="AviStdIndexEntry"/> or <see cref="AviSuperIndexEntry"/> structure.</summary>
    Default = 0x00,

    /// <summary>The index is a field index chunk.</summary>
    /// <remarks>Note: DirectShow does not support field indexes.</remarks>
    ToField = 0x01,
}
