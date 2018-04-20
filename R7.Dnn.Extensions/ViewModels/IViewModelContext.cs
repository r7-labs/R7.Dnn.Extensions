//
//  IViewModelContext.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Roman M. Yagodin
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
using DotNetNuke.UI.Modules;

namespace R7.Dnn.Extensions.ViewModels
{
    /// <summary>
    /// Context to pass to the viewmodels.
    /// </summary>
    public interface IViewModelContext
    {
        /// <summary>
        /// Gets the local resource file.
        /// </summary>
        /// <value>The local resource file.</value>
        string LocalResourceFile { get; }

        /// <summary>
        /// Gets the module context.
        /// </summary>
        /// <value>The module context.</value>
        ModuleInstanceContext Module { get; }

        /// <summary>
        /// Gets the current timestamp.
        /// </summary>
        /// <value>The current timestamp.</value>
        DateTime Now { get; }

        /// <summary>
        /// Gets the current timestamp in UTC.
        /// </summary>
        /// <value>The current timestamp in UTC.</value>
        DateTime UtcNow { get; }

        /// <summary>
        /// Gets localized string what matches provided resource key.
        /// </summary>
        /// <returns>Localized string.</returns>
        /// <param name="key">Resource key.</param>
        string LocalizeString (string key);

        /// <summary>
        /// Gets localized string what matches provided resource key. If failed, returns default value.
        /// Use it to localize format strings and other critical values.
        /// </summary>
        /// <returns>Localized string or default value.</returns>
        /// <param name="key">Resource key.</param>
        /// <param name="defaultValue">Default value.</param>
        string SafeLocalizeString (string key, string defaultValue);
    }
}