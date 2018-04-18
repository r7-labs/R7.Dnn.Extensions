//
//  ParseHelper.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Roman M. Yagodin
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

using System.ComponentModel;
using DotNetNuke.Common.Utilities;

namespace R7.Dnn.Extensions.Text
{
    public static class ParseHelper
    {
        /// <summary>
        /// Parses specified string value to a nullable, optionally threating 
        /// <see cref="T:DotNetNuke.Common.Utilities.Null" /> special values as nulls
        /// </summary>
        /// <returns>Parsed nullable value.</returns>
        /// <param name="value">String value to parse.</param>
        /// <param name="checkDnnNull">If set to 'true', threat <see cref="T:DotNetNuke.Common.Utilities.Null" /> special values as nulls.</param>
        public static T? ParseToNullable<T> (string value, bool checkDnnNull = false) where T : struct
        {
            var tc = TypeDescriptor.GetConverter (typeof (T));

            try {
                var result = (T) tc.ConvertFrom (value);
                if (checkDnnNull && Null.IsNull (result)) {
                    return null;
                }

                return (T?) result;
            }
            catch {
                return null;
            }
        }
    }
}
