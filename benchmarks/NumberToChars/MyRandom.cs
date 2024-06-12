using System;

namespace NumberToChars;

public static class MyRandom
{
    public static float NextFloat(Random random, double from, double upTo)
    {
        return (float)(random.NextDouble() * (upTo - from) + from);
    }

    public static double NextDouble(Random random, double from, double upTo)
    {
        return random.NextDouble() * (upTo - from) + from;
    }
}
