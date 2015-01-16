//
// IModuleControlExtensions.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2015 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using DotNetNuke.UI.Modules;

namespace DotNetNuke.R7
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
            return DotNetNuke.Common.Globals.LinkClick 
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

