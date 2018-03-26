//
//  FormatHelper.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014-2018 Roman M. Yagodin
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R7.Dnn.Extensions.Text
{
    public static class FormatHelper
    {
        /// <summary>
        /// Formats the list of arguments, excluding null or empty ones.
        /// </summary>
        /// <returns>Formatted list.</returns>
        /// <param name="separator">Separator.</param>
        /// <param name="args">Arguments.</param>
        [Obsolete ("Use JoinNotNullOrEmpty instead")]
        public static string FormatList (string separator, params object [] args)
        {
            return FormatList (separator, (IEnumerable) args);
        }

        // TODO: Benchmark this
        /// <summary>
        /// Formats the list of arguments, excluding null or empty ones.
        /// </summary>
        /// <returns>Formatted list.</returns>
        /// <param name="separator">Separator.</param>
        /// <param name="args">Arguments.</param>
        [Obsolete ("Use JoinNotNullOrEmpty instead")]
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

        // TODO: Benchmark this
        /// <summary>
        /// Joins the not null or empty string representations of the objects in the collection using specified separator.
        /// </summary>
        /// <returns>The joined string.</returns>
        /// <param name="separator">Separator.</param>
        /// <param name="values">Values.</param>
        public static string JoinNotNullOrEmpty (string separator, IEnumerable<object> values)
        {
            return string.Join (separator, values.Where (v => v != null)
                                                 .Select (v => v.ToString ())
                                                 .Where (vs => vs.Length > 0)
            );
        }

        /// <summary>
        /// Joins the not null or empty strings in the collection using specified separator.
        /// </summary>
        /// <returns>The joined string.</returns>
        /// <param name="separator">Separator.</param>
        /// <param name="values">Values.</param>
        public static string JoinNotNullOrEmpty (string separator, IEnumerable<string> values)
        {
            return string.Join (separator, values.Where (v => !string.IsNullOrEmpty (v)));
        }

        /// <summary>
        /// Joins the not null or empty string representations of the objects using specified separator.
        /// </summary>
        /// <returns>The joined string.</returns>
        /// <param name="separator">Separator.</param>
        /// <param name="values">Values.</param>
        public static string JoinNotNullOrEmpty (string separator, params object [] values)
        {
            return JoinNotNullOrEmpty (separator, (IEnumerable<object>) values);
        }
        /// <summary>
        /// Joins the not null or empty strings using specified separator.
        /// </summary>
        /// <returns>The joined string.</returns>
        /// <param name="separator">Separator.</param>
        /// <param name="values">Values.</param>
        public static string JoinNotNullOrEmpty (string separator, params string [] values)
        {
            return JoinNotNullOrEmpty (separator, (IEnumerable<string>) values);
        }
    }
}
