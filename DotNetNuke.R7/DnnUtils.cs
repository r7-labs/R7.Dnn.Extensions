//
// Utils.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014 
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
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.UI.Modules;
using DotNetNuke.UI.Skins;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Localization;
using DotNetNuke.Web.UI.WebControls;

namespace DotNetNuke.R7
{
    public class DnnUtils
    {
        public static string GetUserDisplayName (int userId)
        {
            var portalId = PortalController.GetCurrentPortalSettings ().PortalId;
            var user = UserController.GetUserById (portalId, userId);

            // TODO: "System" user name needs localization
            return (user != null) ? user.DisplayName : "System";
        }

        /// <summary>
        /// Determines if the specified file is an images.
        /// </summary>
        /// <returns></returns>
        /// <param name="fileName">File name.</param>
        public static bool IsImage (string fileName)
        {
            if (!string.IsNullOrWhiteSpace (fileName))
                return Globals.glbImageFileTypes.Contains (
                    Path.GetExtension (fileName).Substring (1).ToLowerInvariant ());
            else
                return false;
        }
    } // class
} // namespace
