//
//  LevenshteinDistanceBase.cs
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

namespace R7.Dnn.Extensions.Text.Levenstein
{
    /// <summary>
    /// Abstract class for various Levenshtein distance implementations.
    /// </summary>
    public abstract class LevenshteinDistanceBase
    {
        /// <summary>
        /// The first string.
        /// </summary>
        protected string s1;

        /// <summary>
        /// The second string.
        /// </summary>
        protected string s2;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.Dnn.Extensions.Text.Levenstein.LevenshteinDistanceBase"/> class.
        /// </summary>
        /// <param name="s1">First string.</param>
        /// <param name="s2">Second string.</param>
        protected LevenshteinDistanceBase (string s1, string s2)
        {
            this.s1 = s1;
            this.s2 = s2;
        }

        /// <summary>
        /// Then implemented, calculates the Levenstein distance between two strings.
        /// </summary>
        /// <value>The Levenstein distance.</value>
        public abstract int Distance { get; }

        /// <summary>
        /// Gets the normalized Levenstein distance.
        /// </summary>
        /// <value>The normalized Levenstein distance [0-1].</value>
        public double NormalDistance {
            get {
                var l1 = (s1 == null) ? 0 : s1.Length;
                var l2 = (s2 == null) ? 0 : s2.Length;

                return 1 - (double) Distance / Math.Max (l1, l2);
            }
        }
    }
}

