//
// PagingControlBase.cs
//
// Copyright (c) 2002-2010 DotNetNuke Corporation
// Copyright (c) 2015-2020 Roman M. Yagodin <roman.yagodin@gmail.com>
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
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using R7.Dnn.Extensions.Common;

namespace R7.Dnn.Extensions.Controls.PagingControl
{
    public abstract class PagingControlBase: WebControl, IPostBackEventHandler
    {
        public event EventHandler PageChanged;

        #region Public Properties

        public int PageLinksPerPage { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;

        public PagingControlMode Mode { get; set; } = PagingControlMode.URL;

        public int PageSize { get; set; } = 10;

        public string QuerystringParams { get; set; }

        public int TabID { get; set; } = -1;

        public int TotalRecords { get; set; }

        public bool ShowFirstLast { get; set; }

        public bool ShowStatus { get; set; }

        public string StatusCssClass { get; set; }

        public string ListCssClass { get; set; }

        public string ItemCssClass { get; set; }

        public string LinkCssClass { get; set; }

        public string InactiveItemCssClass { get; set; }

        public string CurrentItemCssClass { get; set; }

        public string NextText { get; set; }

        public string PrevText { get; set; }

        public string FirstText { get; set; }

        public string LastText { get; set; }

        public string StatusFormat { get; set; }

        public string CurrentText { get; set; }

        public string AriaLabel { get; set; }

        #endregion

        #region Text Helper Methods

        protected string GetNextText () => !string.IsNullOrEmpty (NextText) ? NextText : "Next";

        protected string GetPrevText () => !string.IsNullOrEmpty (PrevText) ? PrevText : "Previous";

        protected string GetFirstText () => !string.IsNullOrEmpty (FirstText) ? FirstText : "First";

        protected string GetLastText () => !string.IsNullOrEmpty (LastText) ? LastText : "Last";

        protected string GetStatusFormat () => !string.IsNullOrEmpty (StatusFormat) ? StatusFormat : "Page {0} of {1}";

        protected string GetCurrentText () => !string.IsNullOrEmpty (CurrentText) ? CurrentText : "(current)";

        protected string GetAriaLabel () => !string.IsNullOrEmpty (AriaLabel) ? AriaLabel : "Pagination";

        // TODO: Public interface to get default strings?

        #endregion

        #region Protected Methods

        protected string GetUrl (int currentPage)
        {
            if (Mode == PagingControlMode.URL) {
                if (!string.IsNullOrEmpty (QuerystringParams)) {
                    if (currentPage > 0) {
                        return Globals.NavigateURL (TabID, "", QuerystringParams, "currentpage=" + currentPage);
                    }
                    return Globals.NavigateURL (TabID, "", QuerystringParams);
                }
                if (currentPage > 0) {
                    return Globals.NavigateURL (TabID, "", "currentpage=" + currentPage);
                }
                return Globals.NavigateURL (TabID);
            }

            return Page.ClientScript.GetPostBackClientHyperlink (this, "Page_" + currentPage, false);
        }

        protected int GetTotalPages ()
        {
            return PagingHelper.GetTotalPages (TotalRecords, PageSize);
        }

        protected void OnPageChanged (EventArgs e)
        {
            if (PageChanged != null) {
                PageChanged (this, e);
            }
        }

        #endregion

        public void RaisePostBackEvent (string eventArgument)
        {
            CurrentPage = int.Parse (eventArgument.Replace ("Page_", ""));

            OnPageChanged (new EventArgs ());
        }
    }
}
