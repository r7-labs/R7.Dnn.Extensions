//
//  TypeUtils.cs
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
using System.ComponentModel;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;

namespace R7.Dnn.Extensions.Utilities
{
    [Obsolete]
    public static class TypeUtils
    {
        /// <summary>
        /// Parses specified string value to a nullable, optionally threating 
        /// <see cref="T:DotNetNuke.Common.Utilities.Null" /> special values as nulls
        /// </summary>
        /// <returns>Parsed nullable value.</returns>
        /// <param name="value">String value to parse.</param>
        /// <param name="checkDnnNull">If set to 'true' (default), 
        /// threat <see cref="T:DotNetNuke.Common.Utilities.Null" /> special values as nulls.</param>
        [Obsolete ("Use ParseHelper.ParseToNullable instead")]
        public static T? ParseToNullable<T> (string value, bool checkDnnNull = true) where T : struct
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
        public static T? ToNullable<T> (T n) where T : struct
        {
            return Null.IsNull (n) ? null : (T?) n;
        }

        /// <summary>
        /// Determines if the specified n is null or DNN Null
        /// </summary>
        /// <returns><c>true</c> if the specified n is null or DNN Null; otherwise, <c>false</c>.</returns>
        /// <param name="n">N.</param>
        /// <typeparam name="T">Type parameter.</typeparam>
        public static bool IsNull<T> (T? n) where T : struct
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

