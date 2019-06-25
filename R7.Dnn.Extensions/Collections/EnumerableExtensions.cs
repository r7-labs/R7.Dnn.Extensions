//
//  EnumerableExtensions.cs
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

using System.Collections.Generic;
using System.Linq;

namespace R7.Dnn.Extensions.Collections
{
    /// <summary>
    /// Extensions methods for <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the elemerable is null or contains no elements.
        /// </summary>
        /// <typeparam name="T">The type of IEnumerable elements.</typeparam>
        /// <param name="enumerable">The enumerable, which may be null or empty.</param>
        /// <returns><c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T> (this IEnumerable<T> enumerable)
        {
            if (enumerable == null) {
                return true;
            }

            // if this is a collection, use the O(1) Count property
            var collection = enumerable as ICollection<T>;
            if (collection != null) {
                return collection.Count == 0;
            }

            return !enumerable.Any ();
        }

        /// <summary>
        /// Determines whether the collection is null or contains no elements.
        /// </summary>
        /// <typeparam name="T">The type of ICollection elements.</typeparam>
        /// <param name="collection">The collection, which may be null or empty.</param>
        /// <returns><c>true</c> if the ICollection is null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T> (this ICollection<T> collection)
        {
            return (collection == null) || (collection.Count == 0);
        }
    }
}
