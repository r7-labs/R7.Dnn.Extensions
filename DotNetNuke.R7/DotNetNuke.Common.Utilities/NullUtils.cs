//
// TypeUtils.cs
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
using System.Web.UI.WebControls;

namespace DotNetNuke.Common.Utilities
{
    public class NullUtils
    {
        /// <summary>
        /// Converts n (including DNN Null) to nullable type.
        /// </summary>
        /// <returns>The nullable.</returns>
        /// <param name="n">N.</param>
        /// <typeparam name="T">Type parameter.</typeparam>
        public static Nullable<T> ToNullable<T> (T n) where T: struct
        {
            return Null.IsNull (n) ? null : (Nullable<T>) n;
        }

        /// <summary>
        /// Determines if the specified n is null or DNN Null
        /// </summary>
        /// <returns><c>true</c> if the specified n is null or DNN Null; otherwise, <c>false</c>.</returns>
        /// <param name="n">N.</param>
        /// <typeparam name="T">Type parameter.</typeparam>
        public static bool IsNull<T> (Nullable<T> n) where T: struct
        {
            if (n.HasValue && !Null.IsNull (n.Value))
                return false;

            return true;
        }

        /*
        public static Nullable<T> ParseToNullable<T>(string value) where T: struct
        {
            T n;

            if (Convert.ChangeType(value, typeof(T))
                return Null.IsNull (n)? null : (Nullable<T>) n;
            else
                return null;
        }*/
    }
}

