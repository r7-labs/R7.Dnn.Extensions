//
//  FolderHistory.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Services.FileSystem;
using R7.Dnn.Extensions.Collections;
using R7.Dnn.Extensions.Text;

namespace R7.Dnn.Extensions.FileSystem
{
    /// <summary>
    /// Folder history for user session.
    /// </summary>
    public static class FolderHistory
    {
        /// <summary>
        /// Return last folder id from cookie.
        /// </summary>
        /// <returns>The last folder identifier.</returns>
        /// <param name="request">HTTP request.</param>
        /// <param name="portalId">Portal identifier.</param>
        public static int? GetLastFolderId (HttpRequest request, int portalId)
        {
            var cookie = request.Cookies ["r7_FolderHistory{portalId}"];
            var folderIds = FilterFolders (ParseFolderIds (cookie?.Value));
            if (!folderIds.IsNullOrEmpty ()) {
                return folderIds.Last ();
            }

            // no folders in history, get most recent created folder
            var folder = FolderManager.Instance.GetFolders (portalId).OrderBy (f => f.LastModifiedOnDate).Last ();
            if (folder != null) {
                return folder.FolderID;
            }

            return null;
        }

        /// <summary>
        /// Remembers the folder id in the cookie.
        /// </summary>
        /// <param name="request">HTTP request.</param>
        /// <param name="folderId">Folder identifier.</param>
        /// <param name="portalId">Portal identifier.</param>
        public static void RememberFolder (HttpRequest request, int folderId, int portalId)
        {
            var cookie = request.Cookies ["r7_FolderHistory{portalId}"];
            var folderIds = FilterFolders (ParseFolderIds (cookie?.Value));

            var newCookieValue = cookie?.Value;
            if (!folderIds.IsNullOrEmpty ()) {
                if (folderIds.Last () != folderId) {
                    newCookieValue = FormatHelper.JoinNotNullOrEmpty (";", folderIds.Concat (One (folderId)));
                }
            }
            else {
                newCookieValue = folderId.ToString ();
            }

            cookie = new HttpCookie ("r7_FolderHistory{portalId}") {
                Value = newCookieValue,
                Expires = DateTime.Now.AddHours (24)
            };
            request.Cookies.Add (cookie);
        }

        static IEnumerable<int> One (int i) { yield return i; }

        static IEnumerable<int> ParseFolderIds (string cookieValue)
        {
            if (!string.IsNullOrEmpty (cookieValue)) {
                return cookieValue.Split (new char [] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select (f => int.Parse (f));
            }

            return Enumerable.Empty<int> ();
        }

        static IEnumerable<int> FilterFolders (IEnumerable<int> folderIds)
        {
            foreach (var folderId in folderIds) {
                var folder = FolderManager.Instance.GetFolder (folderId);
                if (folder != null) {
                    yield return folderId;
                }
            }
        }
    }
}
