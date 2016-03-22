//
//  ListControlExtensions.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014, s2015 Roman M. Yagodin
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
using System.Web.UI.WebControls;

namespace DotNetNuke.R7
{
    public static class ListControlExtensions
    {
        /// <summary>
        /// Finds the item index by it's value in ListControl-type list.
        /// </summary>
        /// <returns>Item index.</returns>
        /// <param name="list">List control.</param>
        /// <param name="value">A value.</param>
        /// <param name="defaultIndex">Default index (in case item not found).</param>
        public static int FindIndexByValue (this ListControl list, object value, int defaultIndex = 0)
        { 
            if (value != null)
            {
                var index = 0;
                var strvalue = value.ToString ();
                foreach (ListItem item in list.Items)
                {
                    if (item.Value == strvalue)
                        return index;
                    index++;
                }
            }

            return defaultIndex; 
        }

        /// <summary>
        /// Sets the selected index of ListControl-type list.
        /// </summary>
        /// <param name="list">List control.</param>
        /// <param name="value">A value.</param>
        /// <param name="defaultIndex">Default index (in case item not found).</param>
        public static void SelectByValue (this ListControl list, object value, int defaultIndex = 0)
        {
            list.SelectedIndex = FindIndexByValue (list, value, defaultIndex);
        }
    }
}

