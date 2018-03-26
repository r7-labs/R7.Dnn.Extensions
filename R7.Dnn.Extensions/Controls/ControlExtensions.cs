//
//  ControlExtensions.cs
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

using System.Web.UI;

namespace R7.Dnn.Extensions.Controls
{
    /// <summary>
    /// Control extensions.
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Tries to find direct or indirect parent control of a given type.
        /// </summary>
        /// <returns>The parent control of a given type or null if not found.</returns>
        /// <param name="control">Control.</param>
        /// <typeparam name="T">The type of the parent control.</typeparam>
        public static T FindParentOfType<T> (this Control control) where T : class
        {
            while (control.Parent != null) {
                if (control.Parent is T) {
                    return control.Parent as T;
                }
                control = control.Parent;
            }

            return null;
        }
    }
}

