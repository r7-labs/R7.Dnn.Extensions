//
//  CopyCstor.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
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

namespace R7.Dnn.Extensions.Utilities
{
    /// <summary>
    /// Copy constructor.
    /// </summary>
    public static class CopyCstor
    {
        // TODO: Add/implement as extension method for System.Object?
        /// <summary>
        /// Copy the specified src object properties to dest object.
        /// </summary>
        /// <param name="src">Source object.</param>
        /// <param name="dest">Destination object.</param>
        /// <returns>The dest object filled with properties of src object.</returns>
        /// <typeparam name="T">Common base type for src and dest objects.</typeparam>
        public static T Copy<T> (T src, T dest)
        {
            foreach (var pi in typeof (T).GetProperties ()) {
                if (pi.GetSetMethod () != null) {
                    pi.SetValue (dest, pi.GetValue (src, null), null);
                }
            }

            return dest;
        }
    }       
}
