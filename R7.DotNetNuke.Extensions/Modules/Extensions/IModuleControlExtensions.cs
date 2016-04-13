//
//  IModuleControlExtensions.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014, 2015 Roman M. Yagodin
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
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.Modules;
using DotNetNuke.Common;

namespace R7.DotNetNuke.Extensions
{
    public static class IModuleControlExtensions
    {
        /// <summary>
        /// Formats the URL by DNN rules.
        /// </summary>
        /// <returns>Formatted URL.</returns>
        /// <param name="module">A module reference.</param>
        /// <param name="link">A link value. May be TabID, FileID=something or in other valid forms.</param>
        /// <param name="trackClicks">If set to <c>true</c> then track clicks.</param>
        public static string FormatUrl (this IModuleControl module, string link, bool trackClicks)
        {
            return Globals.LinkClick 
                (link, module.ModuleContext.TabId, module.ModuleContext.ModuleId, trackClicks);
        }

        /// <summary>
        /// Formats the Edit control URL by DNN rules (popups supported).
        /// </summary>
        /// <returns>Formatted Edit control URL.</returns>
        /// <param name="module">A module reference.</param>
        /// <param name="controlKey">Edit control key.</param>
        /// <param name="args">Additional parameters.</param>
        public static string EditUrl (this IModuleControl module, string controlKey, params string [] args)
        {
            // REVIEW: Use 2-element array
            var argList = new List<string> (args); 
            argList.Add ("mid");
            argList.Add (module.ModuleContext.ModuleId.ToString ());

            return module.ModuleContext.NavigateUrl (module.ModuleContext.TabId, controlKey, false, argList.ToArray ());
        }

        public static void SynchronizeModuleHack (this IModuleControl module)
        {
            ModuleController.SynchronizeModule (module.ModuleContext.ModuleId);

            // NOTE: update module cache (temporary fix before 7.2.0)?
            // more info: https://github.com/dnnsoftware/Dnn.Platform/pull/21
            var moduleController = new ModuleController ();
            moduleController.ClearCache (module.ModuleContext.TabId);

        }
    }
}

