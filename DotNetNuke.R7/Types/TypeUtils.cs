//
// TypeUtils.cs
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
using System.ComponentModel;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.R7
{
    public static class TypeUtils
    {
        /// <summary>
        /// Parses specified string value to a nullable int, 
        /// also with convertion of Null.NullInteger to null 
        /// </summary>
        /// <returns>The nullable int.</returns>
        /// <param name="value">String value to parse.</param>
        [Obsolete ("Use T? ParseToNullable<T> () method")]
        public static int? ParseToNullableInt (string value)
        {
            int n;

            if (int.TryParse (value, out n))
                return Null.IsNull (n) ? null : (int?) n;

            return null;
        }

        /// <summary>
        /// Parses specified string value to a nullable, optionally threating 
        /// <see cref="DotNetNuke.Common.Utilities.Null" /> special values as nulls
        /// </summary>
        /// <returns>Parsed nullable value.</returns>
        /// <param name="value">String value to parse.</param>
        /// <param name="checkDnnNull">If set to 'true' (default), 
        /// threat <see cref="DotNetNuke.Common.Utilities.Null" /> special values as nulls.</param>
        public static T? ParseToNullable<T> (string value, bool checkDnnNull = true) where T: struct
        {
            var tc = TypeDescriptor.GetConverter (typeof (T));

            try {
                var result = (T) tc.ConvertFrom (value);

                if (checkDnnNull) {
                    return Null.IsNull (result) ? null : (T?) result;
                }

                return (T?) result;
            }
            catch {
                return null;
            }
        }

        /// <summary>
        /// Converts n (including DNN Null) to nullable type.
        /// </summary>
        /// <returns>The nullable.</returns>
        /// <param name="n">N.</param>
        /// <typeparam name="T">Type parameter.</typeparam>
        public static T? ToNullable<T> (T n) where T: struct
        {
            return Null.IsNull (n) ? null : (T?) n;
        }

        /// <summary>
        /// Determines if the specified n is null or DNN Null
        /// </summary>
        /// <returns><c>true</c> if the specified n is null or DNN Null; otherwise, <c>false</c>.</returns>
        /// <param name="n">N.</param>
        /// <typeparam name="T">Type parameter.</typeparam>
        public static bool IsNull<T> (T? n) where T: struct
        {
            if (n.HasValue && !Null.IsNull (n.Value))
                return false;

            return true;
        }

        public static Unit ToUnit (string value, double minvalue)
        {
            var unit = Unit.Parse (value);
            return unit.Value <= minvalue ? Unit.Empty : unit;
        }
    }
}

