using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberToChars;

public static class TryFormatExtensions
{
    public static Span<char> TryFormat(this int value, Span<char> buffer)
    {
        if (value.TryFormat(buffer, out int charsWritten, format: default, provider: null))
            return buffer.Slice(0, charsWritten);
        else
            throw new ArgumentException("");
    }

    public static Span<char> TryFormat(this float value, Span<char> buffer)
    {
        if (value.TryFormat(buffer, out int charsWritten, format: default, provider: null))
            return buffer.Slice(0, charsWritten);
        else
            throw new ArgumentException("");
    }

    public static Span<char> TryFormat(this double value, Span<char> buffer)
    {
        if (value.TryFormat(buffer, out int charsWritten, format: default, provider: null))
            return buffer.Slice(0, charsWritten);
        else
            throw new ArgumentException("");
    }
}