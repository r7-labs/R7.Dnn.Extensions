//
//  FormatHelperBenchmark.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
