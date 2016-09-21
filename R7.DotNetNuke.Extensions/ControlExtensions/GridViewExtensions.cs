//
//  GridViewExtensions.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016 Roman M. Yagodin
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

using System.Web.UI.WebControls;
using DotNetNuke.Services.Localization;

namespace R7.University.ControlExtensions
{
    public static class GridViewExtensions
    {
        /// <summary>
        /// Localizes the column headers.
        /// </summary>
        /// <param name="gridView">GridView control reference.</param>
        /// <param name="resourceFile">File with localization resources.</param>
        public static void LocalizeHeaders (this GridView gridView, string resourceFile)
        {
            foreach (DataControlField column in gridView.Columns) {
                column.HeaderText = Localization.GetString (column.HeaderText, resourceFile);
            }
        }
    }
}