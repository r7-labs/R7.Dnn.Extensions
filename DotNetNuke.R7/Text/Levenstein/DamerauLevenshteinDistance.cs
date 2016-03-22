//
//  DamerauLevenshteinDistance.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014, 2015 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Text;
using System.Collections.Generic;

namespace DotNetNuke.R7
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

