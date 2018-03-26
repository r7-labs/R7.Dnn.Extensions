//
//  DnnFilePickerUploaderExtensions.cs
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
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Web.UI.WebControls;

namespace R7.Dnn.Extensions.Controls
{
    public static class DnnFilePickerUploaderExtensions
    {
        [Obsolete ("This hack not needed anymore since DNN 7.3")]
        public static void SetFilePathHack (this DnnFilePickerUploader picker, PortalSettings portalSettings)
        {
            if (picker.FileID > 0)
                picker.FilePath = FileManager.Instance.GetUrl (
                    FileManager.Instance.GetFile (picker.FileID))
                    .Remove (0, portalSettings.HomeDirectory.Length);
        }
    }
}

