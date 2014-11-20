//
// StringExtensions.cs
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
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace R7.Common
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
                if (s.Length == 1)
                    return s.ToUpper ();

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
                if (s.Length == 1)
                    return s.ToUpperInvariant ();

                return s.ToUpperInvariant () [0] + s.Substring (1);
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

        public static Unit ToUnit (this string value, double minvalue)
        {
            try
            {
                var unit = Unit.Parse (value);
                if (unit.Value <= minvalue)
                    return Unit.Empty;
                return unit; 
            }
            catch
            {
                return Unit.Empty;
            }
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

