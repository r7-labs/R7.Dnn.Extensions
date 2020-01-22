//
//  PagingHelperTests.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2020 Roman M. Yagodin
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

using R7.Dnn.Extensions.Common;
using Xunit;

namespace R7.Dnn.Extensions.Tests.Common
{
    public class PagingHelperTests
    {
        [Fact]
        public void GetTotalPagesTest ()
        {
            Assert.Equal (0, PagingHelper.GetTotalPages (-1, -1));
            Assert.Equal (0, PagingHelper.GetTotalPages (0, 0));
            Assert.Equal (0, PagingHelper.GetTotalPages (1, 0));
            Assert.Equal (0, PagingHelper.GetTotalPages (0, 1));

            Assert.Equal (1, PagingHelper.GetTotalPages (1, 1));
            Assert.Equal (1, PagingHelper.GetTotalPages (1, 2));

            Assert.Equal (2, PagingHelper.GetTotalPages (2, 1));

            Assert.Equal (2, PagingHelper.GetTotalPages (3, 2));

            Assert.Equal (2, PagingHelper.GetTotalPages (4, 2));
            Assert.Equal (2, PagingHelper.GetTotalPages (4, 3));

            Assert.Equal (3, PagingHelper.GetTotalPages (5, 2));
            Assert.Equal (2, PagingHelper.GetTotalPages (5, 3));
            Assert.Equal (2, PagingHelper.GetTotalPages (5, 4));

            Assert.Equal (3, PagingHelper.GetTotalPages (6, 2));
            Assert.Equal (2, PagingHelper.GetTotalPages (6, 3));
            Assert.Equal (2, PagingHelper.GetTotalPages (6, 4));
            Assert.Equal (2, PagingHelper.GetTotalPages (6, 5));

            Assert.Equal (4, PagingHelper.GetTotalPages (7, 2));
            Assert.Equal (3, PagingHelper.GetTotalPages (7, 3));
            Assert.Equal (2, PagingHelper.GetTotalPages (7, 4));
            Assert.Equal (2, PagingHelper.GetTotalPages (7, 5));
            Assert.Equal (2, PagingHelper.GetTotalPages (7, 6));
        }
    }
}
