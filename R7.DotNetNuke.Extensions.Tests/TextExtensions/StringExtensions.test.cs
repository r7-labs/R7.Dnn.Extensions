//
//  StringExtensions.test.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014 Roman M. Yagodin
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

using System;
using System.Text;
using NUnit.Framework;
using R7.DotNetNuke.Extensions.TextExtensions;

namespace R7.DotNetNuke.Extensions.Tests.TextExtensions
{
    [TestFixture ()]
    public class StringExtensionsTest
    {
        [Test()]
        public void WordCount_TestCase ()
        {
            Assert.AreEqual (0, string.Empty.WordCount ());
            Assert.AreEqual (2, "hello, world!".WordCount ());
            Assert.AreEqual (1, "hello".WordCount ());
        }
    }
}

