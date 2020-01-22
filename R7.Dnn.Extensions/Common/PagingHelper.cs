//
//  PagingHelper.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2020 Roman M. Yagodin
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

using System;

namespace R7.Dnn.Extensions.Common
{
    public static class PagingHelper
    {
        public static int GetTotalPages (int totalRecords, int recordsPerPage)
        {
            if (totalRecords < 1 || recordsPerPage < 1) {
                // input parameters are invalid
                return 0;
            }

            if (recordsPerPage == 1) {
                return totalRecords;
            }

            if (totalRecords <= recordsPerPage) {
                return 1;
            }

            return totalRecords / recordsPerPage + ((totalRecords % recordsPerPage == 0) ? 0 : 1);
        }

        public static Tuple<int,int> GetPagesRange (int totalPages, int pageLinksPerPage, int currentPage)
        {
            var lowNum = 1;
            var highNum = totalPages;

            if (currentPage > (pageLinksPerPage / 2)) {
                var tmpNum = currentPage - pageLinksPerPage / 2.0;
                if (tmpNum < 1) {
                    tmpNum = 1;
                }
                lowNum = Convert.ToInt32 (Math.Floor (tmpNum));
            }

            if (totalPages <= pageLinksPerPage) {
                highNum = totalPages;
            }
            else {
                highNum = lowNum + pageLinksPerPage - 1;
            }

            if (highNum > totalPages) {
                highNum = totalPages;
                if (highNum - lowNum < pageLinksPerPage) {
                    lowNum = highNum - pageLinksPerPage + 1;
                }
            }

            if (lowNum < 1) {
                lowNum = 1;
            }

            return new Tuple<int, int> (lowNum, highNum);
        }
    }
}
