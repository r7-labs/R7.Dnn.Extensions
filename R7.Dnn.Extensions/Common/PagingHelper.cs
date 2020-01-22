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
    }
}
