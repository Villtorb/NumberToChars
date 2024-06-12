using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberToChars.Benchmarks;

public class Options
{
    public static readonly int iterations = 10_000_000;
    public static readonly int seed = 314159;
    public static readonly int bufferSize = 32;
}
