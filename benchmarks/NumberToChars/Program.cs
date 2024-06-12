using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using NumberToChars.Benchmarks;

namespace NumberToChars.Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance;
            var summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
}