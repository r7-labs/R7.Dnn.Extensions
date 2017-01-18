//
//  ViewModelContext_TSettings.cs
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
using System.Web.UI;
using DotNetNuke.UI.Modules;

namespace R7.DotNetNuke.Extensions.ViewModels
{
    /// <summary>
    /// DNN module control context for viewmodels with settings.
    /// </summary>
    public class ViewModelContext<TSettings>: ViewModelContext
        where TSettings: class, new ()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.DotNetNuke.Extensions.ViewModels.ViewModelContext`1"/> class.
        /// </summary>
        /// <param name="module">Module control.</param>
        [Obsolete]
        public ViewModelContext (IModuleControl module): base (module)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.DotNetNuke.Extensions.ViewModels.ViewModelContext`1"/> class.
        /// </summary>
        /// <param name="control">Control.</param>
        /// <param name="module">Module control.</param>
        [Obsolete]
        public ViewModelContext (Control control, IModuleControl module): base (control, module)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.DotNetNuke.Extensions.ViewModels.ViewModelContext`1"/> class.
        /// </summary>
        /// <param name="module">Module control.</param>
        /// <param name="settings">Settings.</param>
        public ViewModelContext (IModuleControl module, TSettings settings): base (module)
        {
            Settings = settings;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.DotNetNuke.Extensions.ViewModels.ViewModelContext`1"/> class.
        /// </summary>
        /// <param name="control">Control.</param>
        /// <param name="module">Module control.</param>
        /// <param name="settings">Settings.</param>
        public ViewModelContext (Control control, IModuleControl module, TSettings settings): base (control, module)
        {
            Settings = settings;
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public TSettings Settings { get; protected set; }
    }
}
