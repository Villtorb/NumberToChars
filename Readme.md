# NumberToChars - C# - int, float, double

![Build Status](https://img.shields.io/github/workflow/status/yourusername/LibraryName/CI)
![License](https://img.shields.io/github/license/yourusername/LibraryName)
![Version](https://img.shields.io/badge/version-1.0.0-blue.svg)

A set of simple functions for converting an `int`, `float` or `double` into a `Span<char>` and avoiding `ToString()` that would otherwise allocate memory.

## Usage

```csharp
using Villt;

    ConvertToChars.Int(i, stackalloc char[32]);
    ConvertToChars.Float(f, stackalloc char[32]);
    ConvertToChars.Double(d, stackalloc char[32]);
    
    // you can force the digit precision with the last parameter
    ConvertToChars.Float(f, stackalloc char[32], 7); //floats are considered precise up to 6-7 digits
    ConvertToChars.Double(d, stackalloc char[32], 16); //doubles are precise up to 15-16 digits

    //extensions
    2024.IntToChars(stackalloc char[4]);
    3.50f.FloatToChars(stackalloc char[4]);
    Math.PI.DoubleToChars(stackalloc char[16]);
```

## Alternatives

<b>TryFormat</b> is a `.NET Core 2.1`, `.Net5` and `.Net6+` feature and is far more convenient, has been available since spans were a thing, but not all of us can use it, such as in Unity as it is using `netstandard2.1`.

For when you can use `TryFormat()`, I recommend this simple extensions method:
```csharp
static Span<char> SpanFormat<T>(this T value, Span<char> buffer, string format = "G", IFormatProvider provider = null) where T : ISpanFormattable
{
    if (value.TryFormat(buffer, out int charsWritten, format, provider))
        return buffer.Slice(0, charsWritten);
    else
        throw new ArgumentException($"Failed to format ({value}) in the format: \"{format}\". Provided Span<Char>[{buffer.Length}] might be too small ");
}
```

```csharp
3.14159f.SpanFormat(stackalloc char[4], "0.00");
// outputs 3.14
```

<b>StringBuilder</b> came in `.Net Core 2.1` but would actually allocate significant memory as it was only later changed to use `TryFormat()` in `.Net5`. 

<b> Cross into the C stdlib - `sprintf` -  </b>
Read more about it [here](https://stackoverflow.com/questions/2479153/using-pinvoke-in-c-sharp-to-call-sprintf-and-friends-on-64-bit/2479210#2479210)

```csharp
using System.Runtime.InteropServices;
using System.Text;

[DllImport("msvcrt.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
static extern int _snwprintf_s([MarshalAs(UnmanagedType.LPWStr)] StringBuilder str, IntPtr bufferSize, IntPtr length, String format, double p);
```

## Benchmarks
The C# compiler has many optimisation tricks for strings and some not so obvious, so highly isolated benchmarks like this should only be recieved as an idea, not a sales statement. Now if anyone can tell me how TryFormat converts ints so fast that'd be great...
<b>
```
 - 10_000_000 random input values
 - buffer_size: 32

| Method                           | Mean        | Error     | StdDev    | Gen0        | Allocated   |
|==== Integers 0 to 2147483647=======================================================================|
| ToString                         |    168.4 ms |   3.14 ms |   2.94 ms | 105750.0000 | 442749300 B |
| TryFormat                        |    124.8 ms |   0.52 ms |   0.49 ms |           - |        80 B |
| IntToChars <--                   |    278.5 ms |   2.67 ms |   2.37 ms |           - |       200 B |

|==== Floats 0.1f to 1f =============================================================================|
| ToString                         | 1,285.12 ms |  7.419 ms |  6.940 ms | 106000.0000 | 445054008 B |
| TryFormat                        | 1,274.84 ms |  6.278 ms |  5.565 ms |           - |       400 B |
| FloatToChars <--                 |   423.34 ms |  1.154 ms |  1.079 ms |           - |       400 B |

|==== Floats 1000f to 9999f =========================================================================|
| ToString                         |  1,614.4 ms |   7.48 ms |   6.25 ms |  95000.0000 | 400044752 B |
| TryFormat                        |  1,580.3 ms |   7.20 ms |   6.73 ms |           - |       400 B |
| FloatToChars <--                 |    209.9 ms |   2.79 ms |   2.61 ms |           - |       133 B |

|==== Doubles 1E+7d to 1E+8d=========================================================================|
| ToString                         | 1,730.43 ms | 20.087 ms | 18.789 ms | 139000.0000 | 581849464 B |
| TryFormat                        | 1,577.45 ms |  2.775 ms |  2.167 ms |           - |       400 B |
| DoubleToChars <--                |   516.69 ms |  2.665 ms |  2.493 ms |           - |       400 B |

|==== Doubles 0.1d to 1d ============================================================================|
| ToString                         | 1,594.30 ms |  3.558 ms |  2.971 ms | 151000.0000 | 633487120 B |
| TryFormat                        | 1,566.58 ms | 13.752 ms | 12.191 ms |           - |       400 B |
| DoubleToChars <--                |   598.73 ms | 11.771 ms | 11.010 ms |           - |       400 B |

|==== Doubles 0d to 1E-15d ==========================================================================|
| ToString                         | 1,878.38 ms | 10.676 ms |  9.464 ms | 158000.0000 | 661490416 B |
| TryFormat                        | 1,813.41 ms | 18.463 ms | 15.418 ms |           - |       400 B |
| DoubleToChars <--                | 1,371.90 ms |  3.734 ms |  3.493 ms |           - |       400 B |
```
</b>
