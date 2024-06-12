using System;
using BenchmarkDotNet.Attributes;

namespace NumberToChars.Benchmarks;

[MemoryDiagnoser]
public class IntToCharsBenchMarks
{
    private readonly int[] randomInts = new int[Options.iterations];

    [GlobalSetup(Targets = new[] { nameof(ToString_0_Max32), nameof(IntToChars_0_Max32), nameof(TryFormat_0_Max32) })]
    public void Int_0_Max32()
    {
        Random r = new Random(Options.seed);
        for (int i = 0; i < Options.iterations; i++)
            randomInts[i] = r.Next();
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
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (int value in randomInts)
            value.TryFormat(buffer);
    }

    [Benchmark]
    public void IntToChars_0_Max32()
    {
        Span<char> buffer = stackalloc char[Options.bufferSize];
        foreach (int value in randomInts)
            ToChars.Int(value, buffer);
    }
}
