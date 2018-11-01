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
using R7.Dnn.Extensions.Urls;

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
            var cookie = request.Cookies [$"r7_FolderHistory{portalId}"];
            var folderIds = FilterDeletedFolders (ParseFolderIds (cookie?.Value));
            if (!folderIds.IsNullOrEmpty ()) {
                return folderIds.Last ();
            }
            return null;
        }

        /// <summary>
        /// Remembers the folder id in the cookie.
        /// </summary>
        /// <param name="request">HTTP request.</param>
        /// <param name="response">HTTP response.</param>
        /// <param name="folderId">Folder identifier.</param>
        /// <param name="portalId">Portal identifier.</param>
        public static void RememberFolder (HttpRequest request, HttpResponse response, int folderId, int portalId)
        {
            var cookie = request.Cookies [$"r7_FolderHistory{portalId}"];
            var folderIds = FilterDeletedFolders (ParseFolderIds (cookie?.Value));

            var newCookieValue = cookie?.Value;
            if (!folderIds.IsNullOrEmpty ()) {
                if (folderIds.Last() != folderId) {
                    newCookieValue = folderIds.Select (f => f.ToString ()).JoinNotNullOrEmpty (",") + "," + folderId;
                }
            }
            else {
                newCookieValue = folderId.ToString ();
            }

            cookie = new HttpCookie ($"r7_FolderHistory{portalId}") {
                Value = newCookieValue,
                Expires = DateTime.Now.AddHours (24)
            };
            response.Cookies.Add (cookie);
        }

        /// <summary>
        /// Remembers the folder by fileid=xxx URL.
        /// </summary>
        /// <param name="request">HTTP request.</param>
        /// <param name="response">HTTP response.</param>
        /// <param name="url">Internal DNN file URL.</param>
        /// <param name="portalId">Portal identifier.</param>
        public static void RememberFolderByFileUrl (HttpRequest request, HttpResponse response, string url, int portalId)
        {
            if (UrlHelper.IsFileUrl (url)) {
                var fileId = UrlHelper.GetResourceId (url);
                if (fileId != null) {
                    var file = FileManager.Instance.GetFile (fileId.Value);
                    if (file != null) {
                        RememberFolder (request, response, file.FolderId, portalId);
                    }
                }
            }
        }

        static IEnumerable<int> ParseFolderIds (string cookieValue)
        {
            if (!string.IsNullOrEmpty (cookieValue)) {
                return cookieValue.Split (new char [] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                  .Select (f => int.Parse (f));
            }

            return Enumerable.Empty<int> ();
        }

        static IEnumerable<int> FilterDeletedFolders (IEnumerable<int> folderIds)
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
