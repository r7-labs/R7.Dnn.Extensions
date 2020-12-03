using System.Collections.Generic;
using System.Reflection;
using R7.Collections;
using Xunit;

namespace R7.Dnn.Extensions.Tests.Collections
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void IsNullOrEmptyTest ()
        {
            var emptyEnumerable = GetEmptyEnumerable ();
            var emptyCollection = new List<object> ();

            Assert.True (emptyEnumerable.IsNullOrEmpty ());
            Assert.True (emptyCollection.IsNullOrEmpty ());

            var enumerable = GetEnumerable ();
            var collection = new List<object> { new object () };

            Assert.False (enumerable.IsNullOrEmpty ());
            Assert.False (collection.IsNullOrEmpty ());

            Assert.True (enumerable.NotNullOrEmpty ());
            Assert.True (collection.NotNullOrEmpty ());
        }

        IEnumerable<object> GetEnumerable ()
        {
            yield return new object ();
        }

        IEnumerable<object> GetEmptyEnumerable ()
        {
            yield break;
        }
    }
}
