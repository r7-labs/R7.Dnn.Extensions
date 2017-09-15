//
//  CultureHelperTests.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
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

using System.Globalization;
using R7.Dnn.Extensions.Utilities;
using Xunit;

namespace R7.Dnn.Extensions.Tests.Utilities
{
    public class CultureHelperTests
    {
        [Fact]
        public void GetPluralIndexTest ()
        {
            var en = new CultureInfo ("en-US");
            Assert.Equal (0, CultureHelper.GetPluralIndex (1, en));
            Assert.Equal (1, CultureHelper.GetPluralIndex (2, en));
            Assert.Equal (1, CultureHelper.GetPluralIndex (10, en));
            Assert.Equal (1, CultureHelper.GetPluralIndex (0, en));

            var ru = new CultureInfo ("ru-RU");
            Assert.Equal (0, CultureHelper.GetPluralIndex (1, ru));
            Assert.Equal (1, CultureHelper.GetPluralIndex (2, ru));
            Assert.Equal (2, CultureHelper.GetPluralIndex (5, ru));
            Assert.Equal (2, CultureHelper.GetPluralIndex (0, ru));
        }
    }
}
