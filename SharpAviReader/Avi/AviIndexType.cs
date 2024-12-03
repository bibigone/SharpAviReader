namespace SharpAviReader.Avi;

/// <summary>The AVI index type.</summary>
internal enum AviIndexType : byte
{
    /// <summary>Each index entry points to another index.</summary>
    /// <remarks> The value of <c>IndexSubType</c> must be zero.</remarks>
    IndexOfIndexes = 0x00,

    /// <summary>Each index entry points to a data chunk in the file.</summary>
    /// <remarks><para>
    /// If <c>IndexSubType</c> is 0, treat the <c>AVIMETAINDEX</c> structure as an <see cref="AviStdIndex"/> structure.
    /// Each index entry is an <see cref="AviStdIndexEntry"/> structure.
    /// </para><para>
    /// If <c>IndexSubType</c> is <c>AVI_INDEX_SUB_2FIELD</c>, the index is a field index chunk.
    /// Note: DirectShow does not support field indexes.
    /// </para></remarks>
    IndexOfChunks = 0x01,

    /// <summary>The index array contains a table of data, not a list of index entries.</summary>
    Data = 0x80,
}
