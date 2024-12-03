using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SharpAviReader;

/// <summary>Represents four character code (FOURCC).</summary>
/// <remarks>FOURCCs are used widely across AVI format.</remarks>
public readonly struct FourCC
        : IEquatable<FourCC>
        , IEquatable<byte[]>
        , IEquatable<char[]>
        , IEquatable<string>
{
    /// <summary>The size of <see cref="FourCC"/> structure in bytes.</summary>
    public const int SIZE = sizeof(uint);

    /// <summary><see cref="uint"/> representation of the code.</summary>
    public readonly uint Value;

    /// <summary>Creates an instance of <see cref="FourCC"/> from <see cref="uint"/> representation.</summary>
    /// <param name="value"><see cref="uint"/> representation of the code.</param>
    public FourCC(uint value)
        => Value = value;

    /// <summary>Creates an instance of <see cref="FourCC"/> from a string representation.</summary>
    /// <param name="code">Four-character code.</param>
    /// <exception cref="ArgumentException">Invalid length of <paramref name="code"/>.</exception>
    public FourCC(string code)
    {
        if (code is null || code.Length != SIZE)
            throw new ArgumentException("Invalid FourCC code", nameof(code));
        Span<byte> bytes = stackalloc byte[SIZE];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)code[i];
        Value = BitConverter.ToUInt32(bytes);
    }

    /// <summary>Creates an instance of <see cref="FourCC"/> from a four-bytes representation.</summary>
    /// <param name="code">Code in four bytes representation.</param>
    /// <exception cref="ArgumentException">Invalid length of <paramref name="code"/>.</exception>
    public FourCC(ReadOnlySpan<byte> code)
    {
        if (code.Length != SIZE)
            throw new ArgumentException("Invalid FourCC code", nameof(code));
        Value = BitConverter.ToUInt32(code);
    }

    /// <summary>Creates an instance of <see cref="FourCC"/> from a four-characters representation.</summary>
    /// <param name="code">Four characters.</param>
    /// <exception cref="ArgumentException">Invalid length of <paramref name="code"/>.</exception>
    public FourCC(ReadOnlySpan<char> code)
    {
        if (code.Length != SIZE)
            throw new ArgumentException("Invalid FourCC code", nameof(code));
        Span<byte> bytes = stackalloc byte[SIZE];
        for (var i = 0; i < bytes.Length; i++)
            bytes[i] = (byte)code[i];
        Value = BitConverter.ToUInt32(bytes);
    }

    /// <inheritdoc/>
    public override string ToString()
        => Encoding.ASCII.GetString(BitConverter.GetBytes(Value));

    /// <inheritdoc/>
    public override int GetHashCode()
        => (int)Value;

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj switch
        {
            null => false,
            ushort v => Value == v,
            FourCC fourCC => Equals(fourCC),
            string str => str is not null && str.Length == SIZE && Equals(new FourCC(str)),
            char[] chr => chr is not null && chr.Length == SIZE && Equals(new FourCC(chr)),
            byte[] bts => bts is not null && bts.Length == SIZE && Equals(new FourCC(bts)),
            _ => throw new NotSupportedException(),
        };

    /// <inheritdoc/>
    public bool Equals(FourCC other)
        => Value == other.Value;

    /// <inheritdoc/>
    public bool Equals([NotNullWhen(true)] byte[]? other)
        => other is not null && other.Length == SIZE && Equals(new FourCC(other));

    /// <inheritdoc/>
    public bool Equals([NotNullWhen(true)] char[]? other)
        => other is not null && other.Length == SIZE && Equals(new FourCC(other));

    /// <inheritdoc/>
    public bool Equals([NotNullWhen(true)] string? other)
        => other is not null && other.Length == SIZE && Equals(new FourCC(other));

    /// <summary>Equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator ==(FourCC a, FourCC b)
        => a.Equals(b);

    /// <summary>Non-equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator !=(FourCC a, FourCC b)
        => !(a == b);

    /// <summary>Equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator ==(FourCC a, [NotNullWhen(true)] string? b)
        => a.Equals(b);

    /// <summary>Non-equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator !=(FourCC a, string? b)
        => !(a == b);

    /// <summary>Equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator ==([NotNullWhen(true)] string? a, FourCC b)
        => b.Equals(a);

    /// <summary>Non-equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator !=(string? a, FourCC b)
        => !(b == a);

    /// <summary>Equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator ==(FourCC a, [NotNullWhen(true)] char[]? b)
        => a.Equals(b);

    /// <summary>Non-equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator !=(FourCC a, char[]? b)
        => !(a == b);

    /// <summary>Equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator ==([NotNullWhen(true)] char[]? a, FourCC b)
        => b.Equals(a);

    /// <summary>Non-equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator !=(char[]? a, FourCC b)
        => !(b == a);

    /// <summary>Equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator ==(FourCC a, [NotNullWhen(true)] byte[]? b)
        => a.Equals(b);

    /// <summary>Non-equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator !=(FourCC a, byte[]? b)
        => !(a == b);

    /// <summary>Equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator ==([NotNullWhen(true)] byte[]? a, FourCC b)
        => b.Equals(a);

    /// <summary>Non-equality.</summary>
    /// <param name="a">Left operand.</param>
    /// <param name="b">Right operand.</param>
    /// <returns>Result.</returns>
    public static bool operator !=(byte[]? a, FourCC b)
        => !(b == a);
}
