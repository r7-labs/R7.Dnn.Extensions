//
//  ViewModelContext.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2015-2017 Roman M. Yagodin
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
using DotNetNuke.UI.Modules;
using UiUtilities = DotNetNuke.Web.UI.Utilities;

namespace R7.DotNetNuke.Extensions.ViewModels
{
    /// <summary>
    /// Simple DNN module control context for viewmodels.
    /// </summary>
    public class ViewModelContext
    {
        /// <summary>
        /// Gets or sets the local resource file.
        /// </summary>
        /// <value>The local resource file.</value>
        public string LocalResourceFile { get; protected set; }

        /// <summary>
        /// Gets or sets the module instance context.
        /// </summary>
        /// <value>The module instance context.</value>
        public ModuleInstanceContext Module { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.DotNetNuke.Extensions.ViewModels.ViewModelContext"/> class.
        /// </summary>
        /// <param name="module">Module control.</param>
        public ViewModelContext (IModuleControl module)
        {
            Module = module.ModuleContext;
            LocalResourceFile = module.LocalResourceFile;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:R7.DotNetNuke.Extensions.ViewModels.ViewModelContext"/> class.
        /// </summary>
        /// <param name="control">Control.</param>
        /// <param name="module">Module control.</param>
        public ViewModelContext (Control control, IModuleControl module)
        {
            Module = module.ModuleContext;
            LocalResourceFile = UiUtilities.GetLocalResourceFile (control);
        }
    }
}
