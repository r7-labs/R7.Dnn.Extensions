using R7.Dnn.Extensions.Text;
using BenchmarkDotNet.Attributes;

namespace R7.Dnn.Extensions.Benchmarks.Text
{
    [MemoryDiagnoser]
    public class FormatHelperBenchmark
    {
        readonly string [] strData = {
            "string 1",
            "string 2",
            "string 3",
            "string 4",
            "string 5",
            "string 6",
            "string 7",
            "string 8",
            "string 9",
            "string 10"
        };

        readonly object [] objData = {
            "string 1",
            "string 2",
            "string 3",
            "string 4",
            "string 5",
            "string 6",
            "string 7",
            "string 8",
            "string 9",
            "string 10"
        };

        [Benchmark]
        public string FormatList_Strings ()
        {
            return FormatHelper.FormatList (", ", strData);
        }

        [Benchmark]
        public string FormatList_Objects ()
        {
            return FormatHelper.FormatList (", ", objData);
        }

        [Benchmark]
        public string JoinNotNullOrEmpty_Strings ()
        {
            return FormatHelper.JoinNotNullOrEmpty (", ", strData);
        }

        [Benchmark]
        public string JoinNotNullOrEmpty_Objects ()
        {
            return FormatHelper.JoinNotNullOrEmpty (", ", objData);
        }
    }
}
