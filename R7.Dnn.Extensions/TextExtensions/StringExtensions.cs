//
//  StringExtensions.cs
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
using System.Text.RegularExpressions;
using R7.Dnn.Extensions.Text.Transliteration;

namespace R7.Dnn.Extensions.TextExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts first character of string to uppercase.
        /// </summary>
        /// <returns>The string with uppercased first character.</returns>
        /// <param name="s">Original string.</param>
        public static string FirstCharToUpper (this string s)
        {
            if (!string.IsNullOrWhiteSpace (s))
            {
                if (s.Length == 1) {
                    return s.ToUpper ();
                }

                return s.ToUpper () [0] + s.Substring (1);
            }

            return s;
        }

        /// <summary>
        /// Converts first character of string to uppercase using invariant culture.
        /// </summary>
        /// <returns>The string with uppercased first character.</returns>
        /// <param name="s">Original string.</param>
        public static string FirstCharToUpperInvariant (this string s)
        {
            if (!string.IsNullOrWhiteSpace (s))
            {
                if (s.Length == 1) {
                    return s.ToUpperInvariant ();
                }

                return s.ToUpperInvariant () [0] + s.Substring (1);
            }

            return s;
        }

        /// <summary>
        /// Converts first character of string to lowercase.
        /// </summary>
        /// <returns>The string with lowercased first character.</returns>
        /// <param name="s">Original string.</param>
        public static string FirstCharToLower (this string s)
        {
            if (!string.IsNullOrWhiteSpace (s))
            {
                if (s.Length == 1) {
                    return s.ToLower ();
                }

                return s.ToLower () [0] + s.Substring (1);
            }

            return s;
        }

        /// <summary>
        /// Converts first character of string to lowercase using invariant culture.
        /// </summary>
        /// <returns>The string with lowercased first character.</returns>
        /// <param name="s">Original string.</param>
        public static string FirstCharToLowerInvariant (this string s)
        {
            if (!string.IsNullOrWhiteSpace (s))
            {
                if (s.Length == 1) {
                    return s.ToLowerInvariant ();
                }

                return s.ToLowerInvariant () [0] + s.Substring (1);
            }

            return s;
        }

        public static int TryExtractInt32 (string text, int defaultValue = 0)
        {
            var matches = Regex.Matches (text, @"\d+");
            if (matches != null && matches.Count > 0)
            {
                int result;
                if (int.TryParse (matches [0].Value, out result))
                    return result;
            }

            return defaultValue;
        }

        public static int ToInt32 (this string value, int defaultValue)
        {
            int tmp;
            return int.TryParse (value, out tmp) ? tmp : defaultValue; 
        }

        public static int WordCount (this string text)
        {
            return Regex.Matches (text, @"\b\w[\w-]*?").Count;
        }

        public static string Transliterate (this string s, TranslitTableBase translitTable)
        {
            if (translitTable != null)
                for (var i = 0; i < translitTable.TranslitTable.GetLength (0); i++)
                    s = Regex.Replace (s, translitTable.TranslitTable [i, 0], translitTable.TranslitTable [i, 1]);

            return s;
        }
    }
}

