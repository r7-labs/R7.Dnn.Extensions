//
// PagingControl.cs
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
using System.IO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using R7.Dnn.Extensions.Common;

// TODO: Abstract, inherit BS4 control from this?
namespace R7.Dnn.Extensions.Controls.PagingControl
{
    public class PagingControl: WebControl, IPostBackEventHandler
    {

        #region "Controls"

        // TODO: Replace table with something
        protected Table tablePageNumbers;

        protected Repeater PageNumbers;

        protected TableCell cellDisplayStatus;

        protected TableCell cellDisplayLinks;

        #endregion

        #region "Private Members"

        int TotalPages = -1;

        string _CSSClassLinkActive;

        string _CSSClassLinkInactive;

        string _CSSClassPagingStatus;

        string _CSSClassLinkCurrent;

        #endregion

        public event EventHandler PageChanged;

        #region "Public Properties"

        public int PageLinksPerPage { get; set; } = 10;

        public string CSSClassLinkActive {
            get {
                if (string.IsNullOrEmpty (_CSSClassLinkActive)) {
                    return "";
                }
                return _CSSClassLinkActive;
            }
            set { _CSSClassLinkActive = value; }
        }

        public string CSSClassLinkInactive {
            get {
                if (string.IsNullOrEmpty (_CSSClassLinkInactive)) {
                    return "disabled";
                }
                return _CSSClassLinkInactive;
            }

            set { _CSSClassLinkInactive = value; }
        }

        public string CSSClassLinkCurrent {
            get {
                if (string.IsNullOrEmpty (_CSSClassLinkCurrent)) {
                    return "active";
                }
                return this._CSSClassLinkCurrent;
            }
            set {
                _CSSClassLinkCurrent = value;
            }
        }

        public string CSSClassPagingStatus {
            get {
                if (string.IsNullOrEmpty (_CSSClassPagingStatus)) {
                    return "Normal";
                }
                return _CSSClassPagingStatus;
            }

            set { _CSSClassPagingStatus = value; }
        }

        public int CurrentPage { get; set; } = 1;

        public PagingControlMode Mode { get; set; } = PagingControlMode.URL;

        public int PageSize { get; set; } = 10;

        public string QuerystringParams { get; set; }

        public int TabID { get; set; } = -1;

        public int TotalRecords { get; set; }

        #endregion

        #region "Private Methods"

        string GetString (string key)
        {
            return DotNetNuke.Services.Localization.Localization.GetString (
                key,
                DotNetNuke.Services.Localization.Localization.SharedResourceFile
            );
        }
       
        // TODO: Extract page calculation methods
        private void BindPageNumbers (int totalRecords, int recordsPerPage)
        {
            TotalPages = PagingHelper.GetTotalPages (totalRecords, recordsPerPage);

            if (TotalPages > 0) {
                var ht = new DataTable ();
                ht.Columns.Add ("PageNum");
                var tmpRow = default (DataRow);

                var LowNum = 1;
                var HighNum = Convert.ToInt32 (TotalPages);

                var tmpNum = 0d;
                tmpNum = CurrentPage - PageLinksPerPage / 2;
                if (tmpNum < 1) {
                    tmpNum = 1;
                }

                if (CurrentPage > (PageLinksPerPage / 2)) {
                    LowNum = Convert.ToInt32 (Math.Floor (tmpNum));
                }

                if (Convert.ToInt32 (TotalPages) <= PageLinksPerPage) {
                    HighNum = Convert.ToInt32 (TotalPages);
                }
                else {
                    HighNum = LowNum + PageLinksPerPage - 1;
                }

                if (HighNum > Convert.ToInt32 (TotalPages)) {
                    HighNum = Convert.ToInt32 (TotalPages);
                    if (HighNum - LowNum < PageLinksPerPage) {
                        LowNum = HighNum - PageLinksPerPage + 1;
                    }
                }

                if (HighNum > Convert.ToInt32 (TotalPages)) {
                    HighNum = Convert.ToInt32 (TotalPages);
                }
                if (LowNum < 1) {
                    LowNum = 1;
                }

                for (var i = LowNum; i <= HighNum; i++) {
                    tmpRow = ht.NewRow ();
                    tmpRow ["PageNum"] = i;
                    ht.Rows.Add (tmpRow);
                }

                PageNumbers.DataSource = ht;
                PageNumbers.DataBind ();
            }
        }

        private string CreateURL (string currentPage)
        {
            if (Mode == PagingControlMode.URL) {
                if (!string.IsNullOrEmpty (QuerystringParams)) {
                    if (!string.IsNullOrEmpty (currentPage)) {
                        return Globals.NavigateURL (TabID, "", QuerystringParams, "currentpage=" + currentPage);
                    }
                    return Globals.NavigateURL (TabID, "", QuerystringParams);
                }
                if (!string.IsNullOrEmpty (currentPage)) {
                    return Globals.NavigateURL (TabID, "", "currentpage=" + currentPage);
                }
                return Globals.NavigateURL (TabID);
            }

            return Page.ClientScript.GetPostBackClientHyperlink (this, "Page_" + currentPage, false);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetLink returns the page number links for paging.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[dancaron]	10/28/2004	Initial Version
        /// </history>
        /// -----------------------------------------------------------------------------
        private string GetLink (int pageNum)
        {
            if (pageNum == CurrentPage)
                return "<li class=\"" + CSSClassLinkCurrent + "\"><span>" + pageNum + "</span></li>";

            return "<li><a href=\"" +
                CreateURL (pageNum.ToString ()) + "\">" + pageNum + "</a></li>";
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetPreviousLink returns the link for the Previous page for paging.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[dancaron]	10/28/2004	Initial Version
        /// </history>
        /// -----------------------------------------------------------------------------
        private string GetPreviousLink ()
        {
            if (CurrentPage > 1 && TotalPages > 0)
                return "<li><a href=\"" + CreateURL ((CurrentPage - 1).ToString ()) + "\">" + GetString ("Previous") + "</a></li>";

            return "<li class=\"" + CSSClassLinkInactive + "\"><span>" + GetString ("Previous") + "</span></li>";
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetNextLink returns the link for the Next Page for paging.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[dancaron]	10/28/2004	Initial Version
        /// </history>
        /// -----------------------------------------------------------------------------
        private string GetNextLink ()
        {
            if (CurrentPage != TotalPages && TotalPages > 0)
                return "<li><a href=\"" + CreateURL ((CurrentPage + 1).ToString ()) + "\">" + GetString ("Next") + "</a></li>";

            return "<li class=\"" + CSSClassLinkInactive + "\"><span>" + GetString ("Next") + "</span></li>";
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetFirstLink returns the First Page link for paging.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[dancaron]	10/28/2004	Initial Version
        /// </history>
        /// -----------------------------------------------------------------------------
        private string GetFirstLink ()
        {
            if (CurrentPage > 1 && TotalPages > 0)
                return "<li><a href=\"" + CreateURL ("1") + "\">" + GetString ("First") + "</a></li>";

            return "<li class=\"" + CSSClassLinkInactive + "\"><span>" + GetString ("First") + "</span></li>";
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetLastLink returns the Last Page link for paging.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[dancaron]	10/28/2004	Initial Version
        /// </history>
        /// -----------------------------------------------------------------------------
        private string GetLastLink ()
        {
            if (CurrentPage != TotalPages && TotalPages > 0)
                return "<li><a href=\"" + CreateURL (TotalPages.ToString ()) + "\">" + GetString ("Last") + "</a></li>";

            return "<li class=\"" + CSSClassLinkInactive + "\"><span>" + GetString ("Last") + "</span></li>";
        }

        #endregion

        protected override void CreateChildControls ()
        {
            tablePageNumbers = new Table ();

            // TODO: Remove this
            tablePageNumbers.Width = new Unit ("100%");

            cellDisplayStatus = new TableCell ();
            cellDisplayLinks = new TableCell ();
            cellDisplayStatus.CssClass = "Normal";
            cellDisplayLinks.CssClass = "Normal";

            if (string.IsNullOrEmpty (CssClass)) {
                tablePageNumbers.CssClass = "PagingTable";
            }
            else {
                tablePageNumbers.CssClass = CssClass;
            }

            var intRowIndex = tablePageNumbers.Rows.Add (new TableRow ());

            PageNumbers = new Repeater ();
            PageNumbers.ItemTemplate = new PageNumberLinkTemplate (this);
            BindPageNumbers (TotalRecords, PageSize);

            // TODO: Remove this
            cellDisplayStatus.HorizontalAlign = HorizontalAlign.Left;

            // TODO: Remove this
            cellDisplayStatus.Style.Add (HtmlTextWriterStyle.WhiteSpace, "nowrap");

            // TODO: Remove this
            cellDisplayLinks.HorizontalAlign = HorizontalAlign.Right;

            // TODO: Remove this
            //cellDisplayLinks.Width = new Unit ("100%");

            var intTotalPages = TotalPages;
            if (intTotalPages == 0) {
                intTotalPages = 1;
            }

            var lit = new LiteralControl (
                string.Format (GetString ("Pages"), CurrentPage.ToString (), intTotalPages.ToString ()));
            cellDisplayStatus.Controls.Add (lit);

            tablePageNumbers.Rows [intRowIndex].Cells.Add (cellDisplayStatus);
            tablePageNumbers.Rows [intRowIndex].Cells.Add (cellDisplayLinks);
        }

        protected void OnPageChanged (EventArgs e)
        {
            if (PageChanged != null) {
                PageChanged (this, e);
            }
        }

        protected override void Render (HtmlTextWriter writer)
        {
            if (PageNumbers == null) {
                CreateChildControls ();
            }

            System.Text.StringBuilder str = new System.Text.StringBuilder ();
            str.Append ("<ul class=\"pagination\">");
            str.Append (GetFirstLink ());
            str.Append (GetPreviousLink ());
            System.Text.StringBuilder result = new System.Text.StringBuilder (1024);
            PageNumbers.RenderControl (new HtmlTextWriter (new StringWriter (result)));
            str.Append (result.ToString ());
            str.Append (GetNextLink ());
            str.Append (GetLastLink ());
            str.Append ("</ul>");

            cellDisplayLinks.Controls.Add (new LiteralControl (str.ToString ()));

            tablePageNumbers.RenderControl (writer);
        }

        public void RaisePostBackEvent (string eventArgument)
        {
            CurrentPage = int.Parse (eventArgument.Replace ("Page_", ""));

            OnPageChanged (new EventArgs ());
        }

        public class PageNumberLinkTemplate: ITemplate
        {
            PagingControl _PagingControl;

            public PageNumberLinkTemplate (PagingControl ctlPagingControl)
            {
                _PagingControl = ctlPagingControl;
            }

            public void InstantiateIn (Control container)
            {
                var lit = new Literal ();
                lit.DataBinding += BindData;
                container.Controls.Add (lit);
            }

            private void BindData (object sender, EventArgs e)
            {
                var lc = (Literal) sender;
                var container = (RepeaterItem) lc.NamingContainer;
                lc.Text = _PagingControl.GetLink (Convert.ToInt32 (DataBinder.Eval (container.DataItem, "PageNum"))) + "&nbsp;&nbsp;";
            }
        }
    }
}
