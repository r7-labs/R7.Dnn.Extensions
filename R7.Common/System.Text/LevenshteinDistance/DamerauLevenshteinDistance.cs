//
// DamerauLevensteinDistance.cs
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
using System.Collections.Generic;

namespace System.Text
{
    public class DamerauLevenshteinDistance : LevenshteinDistanceBase
    {
        public DamerauLevenshteinDistance (string s1, string s2): base (s1, s2)
        {
        }

        public override int Distance
        {
            get
            {
                // REVIEW: require testing

                // border case processing
                if (string.IsNullOrEmpty (s1))
                {
                    if (string.IsNullOrEmpty (s2))
                        return 0;

                    return s2.Length;
                }

                if (string.IsNullOrEmpty (s2))
                    return s1.Length;

                var D = new int[s1.Length + 1, s2.Length + 1]; // dynamics

                // induction base
                D [0, 0] = int.MaxValue;
                for (var i = 0; i <= s1.Length; i++)
                {
                    D [i + 1, 1] = i;
                    D [i + 1, 0] = int.MaxValue;
                }

                for (var j = 0; j <= s2.Length; j++)
                {
                    D [1, j + 1] = j;
                    D [0, j + 1] = int.MaxValue;
                }

                var lastPosition = new Dictionary<char,int> ();
                foreach (var letter in (s1 + s2))
                    if (!lastPosition.ContainsKey (letter))
                        lastPosition.Add (letter, 0);

                for (var i = 1; i <= s1.Length; i++)
                {
                    var last = 0;
                    for (var j = 1; j <= s2.Length; j++)
                    { 
                        var i2 = lastPosition [s2 [j]];
                        var j2 = last;

                        if (s1 [i] == s2 [j])
                        {
                            D [i + 1, j + 1] = D [i, j];
                            last = j;
                        }
                        else
                        {   
                            D [i + 1, j + 1] = Math.Min (Math.Min (D [i, j], D [i + 1, j]), D [i, j + 1] + 1);
                            D [i + 1, j + 1] = Math.Min (D [i + 1, j + 1], D [i2 + 1, j2 + 1] + (i - i2 - 1) + 1 + (j - j2 - 1));
                            lastPosition [s1 [i]] = i;
                        }
                    }
                }
                return D [s1.Length + 1, s2.Length + 1];
            }
        }  
    }
}

