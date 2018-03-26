//
//  ListItemViewModel.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016-2017 Roman M. Yagodin
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

namespace R7.Dnn.Extensions.ViewModels
{
    /// <summary>
    /// Simple viewmodel class to bind to ListControl
    /// </summary>
    [Obsolete ("Replace with anonymous type", true)]
    public class ListItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.Dnn.Extensions.ViewModels.ListItemViewModel"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="text">Text.</param>
        public ListItemViewModel (string value, string text)
        {
            Value = value;
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.Dnn.Extensions.ViewModels.ListItemViewModel"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="text">Text.</param>
        public ListItemViewModel (object value, string text)
        {
            Value = value.ToString ();
            Text = text;
        }

        /// <summary>
        /// Gets or sets item value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; protected set; }

        /// <summary>
        /// Gets or sets item text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; protected set; }
    }
}
