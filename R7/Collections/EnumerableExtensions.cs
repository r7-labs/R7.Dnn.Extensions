using System.Collections.Generic;
using System.Linq;

namespace R7.Collections
{
    /// <summary>
    /// Extensions methods for <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether the enumerable is null or contains no elements.
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
        /// Determines whether the enumerable contains elements.
        /// </summary>
        /// <typeparam name="T">The type of IEnumerable elements.</typeparam>
        /// <param name="enumerable">The enumerable, which may be null or empty.</param>
        /// <returns><c>true</c> if the IEnumerable is not null or empty; otherwise, <c>false</c>.</returns>
        public static bool NotNullOrEmpty<T> (this IEnumerable<T> enumerable)
        {
            return !enumerable.IsNullOrEmpty ();
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
        
        /// <summary>
        /// Determines whether the collection contains elements.
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns><c>true</c> if the ICollection is not null or empty; otherwise, <c>false</c>.</returns>
        public static bool NotNullOrEmpty<T> (this ICollection<T> collection)
        {
            return !collection.IsNullOrEmpty ();
        }
    }
}
