using System;
using BenchmarkDotNet.Attributes;

namespace NumberToChars.Benchmarks;

[MemoryDiagnoser]
public class FloatToCharsBenchmarks
{
    private readonly float[] randomFloats = new float[Options.iterations];

    private void SetupFloats(float from, float to)
    {
        Random r = new Random(Options.seed);
        for (int i = 0; i < Options.iterations; i++)
            randomFloats[i] = MyRandom.NextFloat(r, from, to);
    }

    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_01f_1f), nameof(FloatToChars_01f_1f), nameof(TryFormat_01f_1f) })]
    public void Float_01f_1f() => SetupFloats(0.1f, 1f);

    [Benchmark]
    public void ToString_01f_1f()
    {
        foreach (float value in randomFloats)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_01f_1f()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (float value in randomFloats)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void FloatToChars_01f_1f()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (float value in randomFloats)
            ToChars.Float(value, buffer);
    }


    #endregion //-------------------------------------------------
    //============================================================
    #region 
    //============================================================

    [GlobalSetup(Targets = new[] { nameof(ToString_1000f_9999f), nameof(TryFormat_1000f_9999f), nameof(FloatToChars_1000f_9999f), })]
    public void Float_1000f_9999f() => SetupFloats(1000f, 9999f);

    [Benchmark]
    public void ToString_1000f_9999f()
    {
        foreach (float value in randomFloats)
            value.ToString();
    }

    [Benchmark]
    public void TryFormat_1000f_9999f()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (float value in randomFloats)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void FloatToChars_1000f_9999f()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (float value in randomFloats)
            ToChars.Float(value, buffer);
    }

    #endregion //-------------------------------------------------
}
