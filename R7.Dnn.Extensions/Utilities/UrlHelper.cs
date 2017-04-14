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

using System.Web;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;

namespace R7.Dnn.Extensions.Utilities
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
        public static bool IsInPopup (HttpRequest request)
        {
            var popupArg = request.QueryString ["popup"];
            if (string.IsNullOrEmpty (popupArg)) {
                return false;
            }

            bool popupValue;
            if (bool.TryParse (popupArg, out popupValue)) {
                return popupValue;
            }

            return false;
        }
    }
}
