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

namespace R7.Dnn.Extensions.ControlExtensions
{
    public static class GridViewExtensions
    {
        /// <summary>
        /// Localizes the column headers.
        /// </summary>
        /// <param name="gridView">GridView control reference.</param>
        /// <param name="resourceFile">File with localization resources.</param>
        public static void LocalizeColumnHeaders (this GridView gridView, string resourceFile)
        {
            foreach (DataControlField column in gridView.Columns) {
                column.HeaderText = Localization.GetString (column.HeaderText, resourceFile);
            }
        }

        /// <summary>
        /// Gets row style for a GridView row
        /// </summary>
        /// <returns>The row style.</returns>
        /// <param name="gv">GridView.</param>
        /// <param name="row">GridView row.</param>
        public static TableItemStyle GetRowStyle (this GridView gv, GridViewRow row)
        {
            switch (row.RowType) {
                case DataControlRowType.DataRow:
                    return (row.DataItemIndex % 2 == 0) ? gv.RowStyle : gv.AlternatingRowStyle;
                case DataControlRowType.Header: return gv.HeaderStyle;
                case DataControlRowType.Footer: return gv.FooterStyle;
                case DataControlRowType.EmptyDataRow: return gv.EmptyDataRowStyle;
                case DataControlRowType.Pager: return gv.PagerStyle;
                default: return new TableItemStyle ();
            }
        }
    }
}