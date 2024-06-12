using System;
using System.Buffers;
using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;
using System.Text;

namespace NumberToChars.Benchmarks;

[MemoryDiagnoser]
public class ToCharsBenchmarks
{
    static readonly int iterations = 10_000_000;
    static readonly int seed = 314159;
    static readonly int bufferSize = 32;

    private readonly float[] randomFloats = new float[iterations];
    private readonly double[] randomDoubles = new double[iterations];
    private readonly int[] randomInts = new int[iterations];

    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_0_Max32), nameof(IntToChars_0_Max32), nameof(TryFormat_0_Max32) })]
    public void Int_0_Max32()
    {
        Random r = new Random(seed);
        for (int i = 0; i < iterations; i++)
            randomDoubles[i] = r.Next();
    }

    [Benchmark]
    public void ToString_0_Max32()
    {
        foreach (int value in randomInts)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_0_Max32()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (int value in randomInts)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void IntToChars_0_Max32()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (int value in randomInts)
            ToChars.Int(value, buffer);
    }

    #endregion //-------------------------------------------------

    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_01f_1f), nameof(FloatToChars_01f_1f), nameof(TryFormat_01f_1f) })]
    public void Float_01f_1f()
    {
        Random r = new Random(seed);
        for (int i = 0; i < iterations; i++)
            randomFloats[i] = NextFloat(r, 0.1f, 1f);
    }

    [Benchmark]
    public void ToString_01f_1f()
    {
        foreach (float value in randomFloats)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_01f_1f()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (float value in randomFloats)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void FloatToChars_01f_1f()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (float value in randomFloats)
            ToChars.Float(value, buffer);
    }


    #endregion //-------------------------------------------------
    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_1000f_9999f), nameof(TryFormat_1000f_9999f), nameof(FloatToChars_1000f_9999f), })]
    public void Float_1000f_9999f()
    {
        Random r = new Random(seed);
        for (int i = 0; i < iterations; i++)
            randomFloats[i] = NextFloat(r, 1000f, 9999f);
    }

    [Benchmark]
    public void ToString_1000f_9999f()
    {
        foreach (float value in randomFloats)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_1000f_9999f()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (float value in randomFloats)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void FloatToChars_1000f_9999f()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (float value in randomFloats)
            ToChars.Float(value, buffer);
    }

    #endregion //-------------------------------------------------
    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_10000000d_99999999d), nameof(DoubleToChars_10000000d_99999999d), nameof(TryFormat_10000000d_99999999d) })]
    public void Double_10000000d_99999999d()
    {
        Random r = new Random(seed);
        for (int i = 0; i < iterations; i++)
            randomDoubles[i] = NextDouble(r, 10000000d, 99999999d);
    }

    [Benchmark]
    public void ToString_10000000d_99999999d()
    {
        foreach (double value in randomDoubles)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_10000000d_99999999d()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (double value in randomDoubles)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void DoubleToChars_10000000d_99999999d()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (double value in randomDoubles)
            ToChars.Double(value, buffer);
    }

    #endregion //-------------------------------------------------
    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_01d_1d), nameof(TryFormat_01d_1d), nameof(DoubleToChars_01d_1d) })]
    public void Double_0d_1d()
    {
        Random r = new Random(seed);
        for (int i = 0; i < iterations; i++)
            randomDoubles[i] = NextDouble(r, 0.1d, 1d);
    }

    [Benchmark]
    public void ToString_01d_1d()
    {
        foreach (double value in randomDoubles)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_01d_1d()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (double value in randomDoubles)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void DoubleToChars_01d_1d()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (double value in randomDoubles)
            ToChars.Double(value, buffer);
    }

    #endregion //-------------------------------------------------
    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_0d_1EMinus15d), nameof(DoubleToChars_0d_1EMinus15d), nameof(TryFormat_0d_1EMinus15d) })]
    public void Double_0d_1EMinus15d()
    {
        Random r = new Random(seed);
        for (int i = 0; i < iterations; i++)
            randomDoubles[i] = NextDouble(r, 0d, 0.00000000000001d);
    }

    [Benchmark]
    public void ToString_0d_1EMinus15d()
    {
        foreach (double value in randomDoubles)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_0d_1EMinus15d()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (double value in randomDoubles)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void DoubleToChars_0d_1EMinus15d()
    {
        Span<char> buffer = stackalloc char[bufferSize];
        foreach (double value in randomDoubles)
            ToChars.Double(value, buffer);
    }


    #endregion //-------------------------------------------------

    static float NextFloat(Random random, double from, double upTo)
    {
        return (float)(random.NextDouble() * (upTo - from) + from);
    }

    static double NextDouble(Random random, double from, double upTo)
    {
        return random.NextDouble() * (upTo - from) + from;
    }
}

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