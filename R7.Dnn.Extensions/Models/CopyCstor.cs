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

namespace R7.Dnn.Extensions.Models
{
    /// <summary>
    /// Copy constructor.
    /// </summary>
    public static class CopyCstor
    {
        // TODO: Make private?
        /// <summary>
        /// Copies the specified src object properties to dest object.
        /// </summary>
        /// <param name="src">Source object.</param>
        /// <param name="dest">Destination object.</param>
        /// <returns>The dest object of type T filled with properties of src object.</returns>
        /// <typeparam name="T">Common base type (e.g. interface) for both objects.</typeparam>
        public static T Copy<T> (T src, T dest) where T : class
        {
            foreach (var pi in typeof (T).GetProperties ()) {
                if (pi.GetSetMethod () != null) {
                    pi.SetValue (dest, pi.GetValue (src, null), null);
                }
            }

            return dest;
        }

        /// <summary>
        /// Creates an object of type T from object of type U.
        /// </summary>
        /// <param name="src">Source object.</param>
        /// <returns>New object of type T filled with properties of src object.</returns>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <typeparam name="U">Common base type (e.g. interface) for both objects.</typeparam>
        public static T New<T, U> (U src)
            where T : class, U, new()
            where U : class
        {
            // incapsulate type cast
            return (T) Copy (src, new T ());
        }
    }
}
