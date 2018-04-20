//
//  UrlHelper.cs
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
using System.Linq;
using System.Web;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;

namespace R7.Dnn.Extensions.Urls
{
    /// <summary>
    /// URL helper.
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Gets the URL for cancel link.
        /// </summary>
        /// <returns>The cancel URL.</returns>
        /// <param name="inPopup">Set to <c>true</c> if the control opened in modal popup.</param>
        /// <param name="refresh">Set to <c>true</c> to refresh page after cancel.</param>
        /// <param name="url">URL to redirect after cancel.</param>
        public static string GetCancelUrl (bool inPopup, bool refresh = false, string url = "")
        {
            if (inPopup) {
                return UrlUtils.ClosePopUp (refresh: refresh, url: url, onClickEvent: false);
            }

            return Globals.NavigateURL ();
        }

        /// <summary>
        /// Check if control is opened in modal popup.
        /// </summary>
        /// <returns><c>true</c>, if control is opened in modal popup, <c>false</c> otherwise.</returns>
        /// <param name="request">HTTP request object.</param>
        [Obsolete ("User UrlUtils.InPopUp() instead")]
        public static bool IsInPopup (HttpRequest request)
        {
            return UrlUtils.InPopUp ();
        }

        static readonly char [] resourceIdSeparator = { '=' };

        /// <summary>
        /// Gets the resource identifier from fileid=xxx or alike DNN URLs.
        /// </summary>
        /// <returns>The resource identifier.</returns>
        /// <param name="url">URL.</param>
        public static int GetResourceId (string url)
        {
            var urlParts = url.Split (resourceIdSeparator, StringSplitOptions.RemoveEmptyEntries);
            if (urlParts.Length == 2) {
                int resourceId;
                if (int.TryParse (urlParts [1], out resourceId)) {
                    return resourceId;
                }
            }

            return Null.NullInteger;
        }

        /// <summary>
        /// Little hack to adjust popup URL parameters w/o reimplementing <see cref="M:DotNetNuke.UI.Modules.ModuleInstanceContext.EditUrl()"/>.
        /// </summary>
        /// <returns>The popup URL.</returns>
        /// <param name="popupUrl">Popup URL.</param>
        /// <param name="windowWidth">Window width.</param>
        /// <param name="windowHeight">Window height.</param>
        /// <param name="responseRedirect">If set to <c>true</c> use response redirect when closing popup window.</param>
        public static string AdjustPopupUrl (string popupUrl, int windowWidth = 950, int windowHeight = 550, bool responseRedirect = true)
        {
            return popupUrl.Replace (",550,950,true,'')", $",{windowHeight},{windowWidth},{responseRedirect.ToString ().ToLowerInvariant ()},'')");
        }

        /// <summary>
        /// Checks if browser is InternetExplorer
        /// </summary>
        /// <returns><c>true</c>, if browser is InternetExplorer, <c>false</c> otherwise.</returns>
        /// <param name="request">Request.</param>
        public static bool IsIeBrowser (HttpRequest request)
        {
            var browserName = request.Browser.Browser.ToUpperInvariant ();
            if (browserName.StartsWith ("IE", StringComparison.Ordinal)
                || browserName.Contains ("MSIE")
                || browserName == "INTERNETEXPLORER") {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if browser is Edge
        /// </summary>
        /// <returns><c>true</c>, if browser is Edge, <c>false</c> otherwise.</returns>
        /// <param name="request">Request.</param>
        public static bool IsEdgeBrowser (HttpRequest request)
        {
            return request.UserAgent.Contains ("Edge");
        }

        /// <summary>
        /// Wrapper for <see cref="Globals.NavigateURL()" /> to execute outside HttpContext or on another portal.
        /// </summary>
        /// <returns>The URL.</returns>
        /// <param name="tabId">Tab identifier.</param>
        /// <param name="portalId">Portal identifier.</param>
        public static string NavigateUrl (int tabId, int portalId)
        {
            return NavigateUrl (tabId, portalId, string.Empty, null);
        }

        /// <summary>
        /// Wrapper for <see cref="Globals.NavigateURL()" /> to execute outside HttpContext or on another portal. 
        /// </summary>
        /// <returns>The URL.</returns>
        /// <param name="tabId">Tab identifier.</param>
        /// <param name="portalId">Portal identifier.</param>
        /// <param name="controlKey">Control key.</param>
        /// <param name="additionalParameters">Additional parameters.</param>
        public static string NavigateUrl (int tabId, int portalId, string controlKey, params string [] additionalParameters)
        {
            return Globals.NavigateURL (tabId,
                                        new PortalSettings (tabId, GetPrimaryPortalAlias (portalId)),
                                        controlKey, additionalParameters);
        }

        static PortalAliasInfo GetPrimaryPortalAlias (int portalId)
        {
            var portalAliases = PortalAliasController.Instance.GetPortalAliasesByPortalId (portalId);
            var portalAlias = portalAliases.FirstOrDefault (pa => pa.IsPrimary);
            if (portalAlias == null) {
                // fallback to first available portal alias
                portalAlias = portalAliases.FirstOrDefault ();
            }

            return portalAlias;
        }
    }
}
