using BenchmarkDotNet.Running;
using R7.Dnn.Extensions.Benchmarks.Text;

namespace R7.Dnn.Extensions.Benchmarks
{
    class Program
    {
        public static void Main (string [] args)
        {
            var summary = BenchmarkRunner.Run<FormatHelperBenchmark> ();
        }
    }
}
