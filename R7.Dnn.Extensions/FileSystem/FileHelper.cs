//
//  FileHelper.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014-2018 Roman M. Yagodin
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
using System.IO;
using DotNetNuke.Common;
using DotNetNuke.Entities.Controllers;

namespace R7.Dnn.Extensions.FileSystem
{
    public static class FileHelper
    {
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

            return false;
        }

        public static bool IsFileAllowed (string filename)
        {
            var hostSettings = HostController.Instance.GetSettingsDictionary ();
            var allowedFileExts = hostSettings ["FileExtensions"].Split (new [] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            var upExt = Path.GetExtension (filename).ToLowerInvariant ().TrimStart ('.');
            foreach (var ext in allowedFileExts)
                if (ext.Trim ().ToLowerInvariant () == upExt)
                    return true;

            return false;
        }
    }
}

