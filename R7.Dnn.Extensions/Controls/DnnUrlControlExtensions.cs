//
//  DnnUrlControlExtensions.cs
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

using System.IO;
using System.Linq;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Web.UI.WebControls;

namespace R7.Dnn.Extensions.Controls
{
    public static class DnnUrlControlExtensions
    {
        /// <summary>
        /// Selects the folder in DnnUrlControl by folder id.
        /// </summary>
        /// <returns><c>true</c>, if folder was selected successfully, <c>false</c> otherwise.</returns>
        /// <param name="urlControl">DNN URL control.</param>
        /// <param name="folderId">Folder identifier.</param>
        /// <param name="createFile">If set to <c>true</c>, will try to create file in the empty folder to allow select it.</param>
        public static bool SelectFolder (this DnnUrlControl urlControl, int folderId, bool createFile = false)
        {
            try {
                var folder = FolderManager.Instance.GetFolder (folderId);
                if (folder != null) {
                    var file = FolderManager.Instance.GetFiles (folder).FirstOrDefault ();
                    // try to create file if folder is empty
                    if (file == null && createFile) {
                        file = FileManager.Instance.AddFile (folder, "DefaultFile", new MemoryStream ());
                    }
                    if (file != null) {
                        urlControl.Url = "fileid=" + file.FileId;
                        return true;
                    }
                }
            }
            catch {
            }

            return false;
        }
    }
}
