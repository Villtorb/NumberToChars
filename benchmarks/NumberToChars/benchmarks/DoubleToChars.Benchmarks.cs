using System;
using BenchmarkDotNet.Attributes;

namespace NumberToChars.Benchmarks;

[MemoryDiagnoser]
public class DoubleToCharsBenchmarks
{
    private readonly double[] randomDoubles = new double[Options.iterations];

    public void SetupDoubles(double from, double to)
    {
        Random r = new Random(Options.seed);
        for (int i = 0; i < Options.iterations; i++)
            randomDoubles[i] = MyRandom.NextDouble(r, from, to);
    }

    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_10000000d_99999999d), nameof(DoubleToChars_10000000d_99999999d), nameof(TryFormat_10000000d_99999999d) })]
    public void Double_10000000d_99999999d() => SetupDoubles(10000000d, 99999999d);

    [Benchmark]
    public void ToString_10000000d_99999999d()
    {
        foreach (double value in randomDoubles)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_10000000d_99999999d()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (double value in randomDoubles)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void DoubleToChars_10000000d_99999999d()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (double value in randomDoubles)
            ToChars.Double(value, buffer);
    }

    #endregion //-------------------------------------------------
    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_01d_1d), nameof(TryFormat_01d_1d), nameof(DoubleToChars_01d_1d) })]
    public void Double_0d_1d() => SetupDoubles(0.1d, 1d);

    [Benchmark]
    public void ToString_01d_1d()
    {
        foreach (double value in randomDoubles)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_01d_1d()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (double value in randomDoubles)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void DoubleToChars_01d_1d()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (double value in randomDoubles)
            ToChars.Double(value, buffer);
    }

    #endregion //-------------------------------------------------
    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_0d_1EMinus15d), nameof(DoubleToChars_0d_1EMinus15d), nameof(TryFormat_0d_1EMinus15d) })]
    public void Double_0d_1EMinus15d() => SetupDoubles(0d, 0.00000000000001d);

    [Benchmark]
    public void ToString_0d_1EMinus15d()
    {
        foreach (double value in randomDoubles)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_0d_1EMinus15d()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (double value in randomDoubles)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void DoubleToChars_0d_1EMinus15d()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (double value in randomDoubles)
            ToChars.Double(value, buffer);
    }

    #endregion //-------------------------------------------------
}

