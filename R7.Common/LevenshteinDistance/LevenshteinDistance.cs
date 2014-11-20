//
// LevensteinDistance.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Security.Cryptography;

namespace R7.Common
{
    public class LevenshteinDistance : LevenshteinDistanceBase
    {
        public LevenshteinDistance (string s1, string s2) : base (s1, s2)
        {
        }

        public override int Distance
        {
            get
            {
                if (string.IsNullOrEmpty (s1))
                {
                    if (string.IsNullOrEmpty (s2))
                        return 0;
                    return s2.Length;
                }

                if (string.IsNullOrEmpty (s2))
                    return s1.Length;

                var diff = 0;                       
                int [,] m = new int[s1.Length + 1, s2.Length + 1];

                for (var i = 0; i <= s1.Length; i++)
                    m [i, 0] = i;
                for (var j = 0; j <= s2.Length; j++)
                    m [0, j] = j;

                for (var i = 1; i <= s1.Length; i++)
                    for (var j = 1; j <= s2.Length; j++)
                    {
                        diff = (s1 [i - 1] == s2 [j - 1]) ? 0 : 1;
                        m [i, j] = Math.Min (
                            Math.Min (m [i - 1, j] + 1, m [i, j - 1] + 1), m [i - 1, j - 1] + diff);
                    }

                return m [s1.Length, s2.Length];   
            }
        }
    }
}
