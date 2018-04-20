//
//  EnumerableExtensionsTests.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using R7.Dnn.Extensions.Collections;
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
