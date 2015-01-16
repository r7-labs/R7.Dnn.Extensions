//
// TextUtils.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2015 
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
using System.Collections;

namespace DotNetNuke.R7
{
    public static class TextUtils
    {
        /// <summary>
        /// Formats the list of arguments, excluding empty ones.
        /// </summary>
        /// <returns>Formatted list.</returns>
        /// <param name="separator">Separator.</param>
        /// <param name="args">Arguments.</param>
        public static string FormatList (string separator, params object [] args)
        {
            var sb = new StringBuilder (args.Length);

            var i = 0;
            foreach (var a in args)
            {
                if (!string.IsNullOrWhiteSpace (a.ToString ()))
                {
                    if (i++ > 0)
                        sb.Append (separator);

                    sb.Append (a);
                }
            }

            return sb.ToString ();
        }

        public static string FormatList (string separator, IEnumerable args)
        {
            var sb = new StringBuilder ();

            var i = 0;
            foreach (var a in args)
            {
                if (a != null && !string.IsNullOrWhiteSpace (a.ToString ()))
                {
                    if (i++ > 0)
                        sb.Append (separator);

                    sb.Append (a);
                }
            }

            return sb.ToString ();
        }
    }
}

