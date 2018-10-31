//
//  UrlHistory.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
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
using System.Web.SessionState;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.FileSystem;
using R7.Dnn.Extensions.Urls;

namespace R7.Dnn.Extensions.UrlHistory
{
    /// <summary>
    /// Manages URL history across session.
    /// </summary>
    [Obsolete ("Consider using FolderHistory")]
    public class UrlHistory
    {
        protected readonly UrlHistoryBackend Backend;

        public UrlHistory (HttpSessionState session, string variableName = "r7_Shared_UrlHistory")
        {
            if (session.Mode == SessionStateMode.InProc) {
                Backend = new UrlHistoryInProcBackend ();
            }
            else {
                Backend = new UrlHistoryDefaultBackend ();
            }

            Backend.Init (session, variableName);
        }

        public void StoreUrl (string url)
        {
            if (string.IsNullOrEmpty (url)) {
                return;
            }

            Backend.StoreUrl (url);
        }

        public IList<ListItem> GetBindableUrls ()
        {
            var portalId = PortalSettings.Current.PortalId;
            var urlList = new List<ListItem> ();
            foreach (var url in Backend.GetUrls ()) {
                urlList.Add (new ListItem (GetUrlName (url, portalId), url));
            }

            return urlList;
        }

        protected virtual string GetUrlName (string url, int portalId)
        {
            switch (Globals.GetURLType (url)) {
                case TabType.File:
                    var file = FileManager.Instance.GetFile (UrlHelper.GetResourceId (url) ?? -1);
                    if (file != null) {
                        return file.Folder + file.FileName;
                    }
                    break;

                case TabType.Tab:
                    var tab = TabController.Instance.GetTab (int.Parse (url), portalId);
                    if (tab != null) {
                        return tab.TabName;
                    }
                    break;

                case TabType.Member:
                    var user = UserController.Instance.GetUserById (portalId, UrlHelper.GetResourceId (url) ?? -1);
                    if (user != null) {
                        return user.DisplayName;
                    }
                    break;
            }

            return url;
        }
    }
}
