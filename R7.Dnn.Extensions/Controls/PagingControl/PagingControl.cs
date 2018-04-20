//
// PagingControl.cs
//
// Copyright (c) 2002-2010 DotNetNuke Corporation
// Copyright (c) 2015 Roman M. Yagodin <roman.yagodin@gmail.com>
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
using System.Data;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DnnLocalization = DotNetNuke.Services.Localization.Localization;

namespace R7.Dnn.Extensions.Controls.PagingControl
{
    [ToolboxData ("<{0}:PagingControl runat=server></{0}:PagingControl>")]
    public class PagingControl: WebControl, IPostBackEventHandler
    {

        #region "Controls"

        protected Table tablePageNumbers;
        protected Repeater PageNumbers;
        protected TableCell cellDisplayStatus;
        protected TableCell cellDisplayLinks;

        #endregion

        #region "Private Members"

        private int _PageLinksPerPage = 10;
        private int TotalPages = -1;
        private int _TotalRecords;
        private PagingControlMode _Mode = PagingControlMode.URL;
        private int _PageSize;// = 1; check DivideByZeroException
        private int _CurrentPage;
        private string _QuerystringParams;
        private int _TabID;
        private string _CSSClassLinkActive;
        private string _CSSClassLinkInactive;
        private string _CSSClassPagingStatus;
        private string _CSSClassLinkCurrent;

        #endregion

        public event EventHandler PageChanged;

        #region "Protected Properties"

        [Bindable (true), Category ("Behavior"), DefaultValue (10)]
        public int PageLinksPerPage {
            get { return this._PageLinksPerPage; }
            set { _PageLinksPerPage = value; }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("")]
        public string CSSClassLinkActive {
            get {
                if (string.IsNullOrEmpty (_CSSClassLinkActive)) {
                    return "";
                }
                else {
                    return _CSSClassLinkActive;
                }
            }
            set { _CSSClassLinkActive = value; }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("disabled")]
        public string CSSClassLinkInactive {
            get {
                if (string.IsNullOrEmpty (_CSSClassLinkInactive)) {
                    return "disabled";
                }
                else {
                    return _CSSClassLinkInactive;
                }
            }

            set { _CSSClassLinkInactive = value; }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("disabled")]
        public string CSSClassLinkCurrent {
            get {
                if (string.IsNullOrEmpty (_CSSClassLinkCurrent))
                    return "active";
                else
                    return this._CSSClassLinkCurrent;
            }
            set {
                _CSSClassLinkCurrent = value;
            }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("Normal")]
        public string CSSClassPagingStatus {
            get {
                if (string.IsNullOrEmpty (_CSSClassPagingStatus)) {
                    return "Normal";
                }
                else {
                    return _CSSClassPagingStatus;
                }
            }

            set { _CSSClassPagingStatus = value; }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("1")]
        public int CurrentPage {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }

        public PagingControlMode Mode {
            get { return _Mode; }
            set { _Mode = value; }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("10")]
        public int PageSize {
            get { return _PageSize; }

            set { _PageSize = value; }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("")]
        public string QuerystringParams {
            get { return _QuerystringParams; }

            set { _QuerystringParams = value; }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("-1")]
        public int TabID {
            get { return _TabID; }

            set { _TabID = value; }
        }

        [Bindable (true), Category ("Behavior"), DefaultValue ("0")]
        public int TotalRecords {
            get { return _TotalRecords; }

            set { _TotalRecords = value; }
        }

        #endregion

        #region "Private Methods"

        private void BindPageNumbers (int TotalRecords, int RecordsPerPage)
        {
            if (TotalRecords < 1 || RecordsPerPage < 1) {
                TotalPages = 1;
                return;
            }

            if (TotalRecords / RecordsPerPage >= 1) {
                TotalPages = TotalRecords / RecordsPerPage + ((TotalRecords % RecordsPerPage == 0) ? 0 : 1);
            }
            else {
                TotalPages = 0;
            }

            if (TotalPages > 0) {
                DataTable ht = new DataTable ();
                ht.Columns.Add ("PageNum");
                DataRow tmpRow = default (DataRow);

                int LowNum = 1;
                int HighNum = Convert.ToInt32 (TotalPages);

                double tmpNum = 0;
                tmpNum = CurrentPage - PageLinksPerPage / 2;
                if (tmpNum < 1)
                    tmpNum = 1;

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

                if (HighNum > Convert.ToInt32 (TotalPages))
                    HighNum = Convert.ToInt32 (TotalPages);
                if (LowNum < 1)
                    LowNum = 1;

                int i = 0;
                for (i = LowNum; i <= HighNum; i++) {
                    tmpRow = ht.NewRow ();
                    tmpRow ["PageNum"] = i;
                    ht.Rows.Add (tmpRow);
                }

                PageNumbers.DataSource = ht;
                PageNumbers.DataBind ();
            }

        }

        private string CreateURL (string CurrentPage)
        {

            if (Mode == PagingControlMode.URL) {
                if (!string.IsNullOrEmpty (QuerystringParams)) {
                    if (!string.IsNullOrEmpty (CurrentPage)) {
                        return Globals.NavigateURL (TabID, "", QuerystringParams, "currentpage=" + CurrentPage);
                    }
                    else {
                        return Globals.NavigateURL (TabID, "", QuerystringParams);
                    }
                }
                else {
                    if (!string.IsNullOrEmpty (CurrentPage)) {
                        return Globals.NavigateURL (TabID, "", "currentpage=" + CurrentPage);
                    }
                    else {
                        return Globals.NavigateURL (TabID);
                    }
                }
            }
            else {
                return this.Page.ClientScript.GetPostBackClientHyperlink (this, "Page_" + CurrentPage.ToString (), false);
            }

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
        private string GetLink (int PageNum)
        {
            if (PageNum == CurrentPage)
                return "<li class=\"" + CSSClassLinkCurrent + "\"><span>" + PageNum + "</span></li>";

            return "<li><a href=\"" +
                CreateURL (PageNum.ToString ()) + "\">" + PageNum + "</a></li>";
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
                return "<li><a href=\"" + CreateURL ((CurrentPage - 1).ToString ()) + "\">" + DnnLocalization.GetString ("Previous", DnnLocalization.SharedResourceFile) + "</a></li>";

            return "<li class=\"" + CSSClassLinkInactive + "\"><span>" + DnnLocalization.GetString ("Previous", DnnLocalization.SharedResourceFile) + "</span></li>";
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
                return "<li><a href=\"" + CreateURL ((CurrentPage + 1).ToString ()) + "\">" + DnnLocalization.GetString ("Next", DnnLocalization.SharedResourceFile) + "</a></li>";

            return "<li class=\"" + CSSClassLinkInactive + "\"><span>" + DnnLocalization.GetString ("Next", DnnLocalization.SharedResourceFile) + "</span></li>";
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
                return "<li><a href=\"" + CreateURL ("1") + "\">" + DnnLocalization.GetString ("First", DnnLocalization.SharedResourceFile) + "</a></li>";

            return "<li class=\"" + CSSClassLinkInactive + "\"><span>" + DnnLocalization.GetString ("First", DnnLocalization.SharedResourceFile) + "</span></li>";
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
                return "<li><a href=\"" + CreateURL (TotalPages.ToString ()) + "\">" + DnnLocalization.GetString ("Last", DnnLocalization.SharedResourceFile) + "</a></li>";

            return "<li class=\"" + CSSClassLinkInactive + "\"><span>" + DnnLocalization.GetString ("Last", DnnLocalization.SharedResourceFile) + "</span></li>";
        }

        #endregion

        protected override void CreateChildControls ()
        {

            tablePageNumbers = new System.Web.UI.WebControls.Table ();

            tablePageNumbers.Width = new Unit ("100%");

            cellDisplayStatus = new System.Web.UI.WebControls.TableCell ();
            cellDisplayLinks = new System.Web.UI.WebControls.TableCell ();
            cellDisplayStatus.CssClass = "Normal";
            cellDisplayLinks.CssClass = "Normal";

            if (string.IsNullOrEmpty (this.CssClass)) {
                tablePageNumbers.CssClass = "PagingTable";
            }
            else {
                tablePageNumbers.CssClass = this.CssClass;
            }

            int intRowIndex = tablePageNumbers.Rows.Add (new TableRow ());

            PageNumbers = new Repeater ();
            PageNumberLinkTemplate I = new PageNumberLinkTemplate (this);
            PageNumbers.ItemTemplate = I;
            BindPageNumbers (TotalRecords, PageSize);

            cellDisplayStatus.HorizontalAlign = HorizontalAlign.Left;

            cellDisplayStatus.Style.Add (HtmlTextWriterStyle.WhiteSpace, "nowrap");

            cellDisplayLinks.HorizontalAlign = HorizontalAlign.Right;
            //cellDisplayLinks.Width = new Unit ("100%");

            int intTotalPages = TotalPages;
            if (intTotalPages == 0)
                intTotalPages = 1;

            string str = null;
            str = string.Format (DnnLocalization.GetString ("Pages"), CurrentPage.ToString (), intTotalPages.ToString ());
            LiteralControl lit = new LiteralControl (str);
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

        protected override void Render (System.Web.UI.HtmlTextWriter output)
        {
            if (PageNumbers == null) {
                CreateChildControls ();
            }

            System.Text.StringBuilder str = new System.Text.StringBuilder ();
            str.Append ("<ul class=\"pagination\">");
            str.Append (GetFirstLink ());
            str.Append (GetPreviousLink ());
            System.Text.StringBuilder result = new System.Text.StringBuilder (1024);
            PageNumbers.RenderControl (new HtmlTextWriter (new System.IO.StringWriter (result)));
            str.Append (result.ToString ());
            str.Append (GetNextLink ());
            str.Append (GetLastLink ());
            str.Append ("</ul>");

            cellDisplayLinks.Controls.Add (new LiteralControl (str.ToString ()));

            tablePageNumbers.RenderControl (output);
        }

        public void RaisePostBackEvent (string eventArgument)
        {
            CurrentPage = int.Parse (eventArgument.Replace ("Page_", ""));

            OnPageChanged (new EventArgs ());
        }

        public class PageNumberLinkTemplate: ITemplate
        {
            //static int itemcount = 0;

            private PagingControl _PagingControl;
            public PageNumberLinkTemplate (PagingControl ctlPagingControl)
            {
                _PagingControl = ctlPagingControl;
            }


            public void InstantiateIn (Control container)
            {
                Literal l = new Literal ();
                l.DataBinding += this.BindData;
                container.Controls.Add (l);
            }

            private void BindData (object sender, System.EventArgs e)
            {
                Literal lc = default (Literal);
                lc = (Literal) sender;
                RepeaterItem container = default (RepeaterItem);
                container = (RepeaterItem) lc.NamingContainer;
                lc.Text = _PagingControl.GetLink (Convert.ToInt32 (DataBinder.Eval (container.DataItem, "PageNum"))) + "&nbsp;&nbsp;";
            }
        }
    }
}
