//
// FileUtils.cs
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
using System.IO;
using DotNetNuke.Common;
using DotNetNuke.Entities.Controllers;
using DotNetNuke.Services.FileSystem;

namespace DotNetNuke.R7
{
    public static class FileUtils
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
                if (ext.Trim().ToLowerInvariant () == upExt)
                    return true;

            return false;
        }
    }
}

