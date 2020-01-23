//
//  PagingControl.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2020 Roman M. Yagodin
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

using System.Text;
using System.Web.UI;
using R7.Dnn.Extensions.Common;

namespace R7.Dnn.Extensions.Controls.PagingControl
{
    // TODO: Icons support w/ proper a11y: https://getbootstrap.com/docs/4.4/components/pagination/#working-with-icons
    public class PagingControl: PagingControlBase
    {
        protected override void Render (HtmlTextWriter writer)
        {
            var totalPages = GetTotalPages ();
            if (totalPages <= 0) {
                // nothing to render
                return;
            }

            var pagesRange = PagingHelper.GetPagesRange (totalPages, PageLinksPerPage, CurrentPage);
            var sb = new StringBuilder (1024);

            sb.Append ($"<nav id=\"{ClientID}\" class=\"{CssClass}\" aria-label=\"{GetAriaLabel ()}\">");

            if (ShowStatus) {
                sb.Append (RenderPagingStatus (totalPages));
            }

            sb.Append ($"<ul class=\"{ListCssClass}\">");

            if (ShowFirstLast) {
                sb.Append (RenderFirstLink (totalPages));
            }

            sb.Append (RenderPreviousLink (totalPages));

            for (var pageNum = pagesRange.Item1; pageNum <= pagesRange.Item2; pageNum++) {
                sb.Append (RenderLink (pageNum));
            }

            sb.Append (RenderNextLink (totalPages));

            if (ShowFirstLast) {
                sb.Append (RenderLastLink (totalPages));
            }

            sb.Append ("</ul></nav>");

            writer.Write (sb);
        }

        /// <summary>
        /// RenderLink renders the paging status markup.
        /// </summary>
        protected virtual string RenderPagingStatus (int totalPages)
        {
            return $"<div class=\"{StatusCssClass}\">{string.Format (GetStatusFormat (), CurrentPage, totalPages)}</div>";
        }

        /// <summary>
        /// RenderLink renders the page number links markup.
        /// </summary>
        protected virtual string RenderLink (int pageNum)
        {
            if (pageNum == CurrentPage) {
                return $"<li class=\"{ItemCssClass} {CurrentItemCssClass}\" aria-current=\"page\">" +
                    $"<span class=\"{LinkCssClass}\">{pageNum} <span class=\"sr-only\">{GetCurrentText ()}</span></span></li>";
            }

            return $"<li class=\"{ItemCssClass}\"><a class=\"{LinkCssClass}\" href=\"{GetUrl (pageNum)}\">{pageNum}</a></li>";
        }

        /// <summary>
        /// RenderPreviousLink renders the Previous Page link markup.
        /// </summary>
        protected virtual string RenderPreviousLink (int totalPages)
        {
            if (CurrentPage > 1 && totalPages > 0) {
                return $"<li class=\"{ItemCssClass}\"><a class=\"{LinkCssClass}\" href=\"{GetUrl (CurrentPage - 1)}\">{GetPrevText ()}</a></li>";
            }

            return $"<li class=\"{ItemCssClass} {InactiveItemCssClass}\">" +
            	$"<span class=\"{LinkCssClass}\">{GetPrevText ()}</span></li>";
        }

        /// <summary>
        /// RenderNextLink renders the Next Page link markup.
        /// </summary>
        protected virtual string RenderNextLink (int totalPages)
        {
            if (CurrentPage != totalPages && totalPages > 0) {
                return $"<li class=\"{ItemCssClass}\"><a class=\"{LinkCssClass}\" href=\"{GetUrl (CurrentPage + 1)}\">{GetNextText ()}</a></li>";
            }

            return $"<li class=\"{ItemCssClass} {InactiveItemCssClass}\">" +
            	$"<span class=\"{LinkCssClass}\">{GetNextText ()}</span></li>";
        }

        /// <summary>
        /// RenderFirstLink renders the First Page link markup.
        /// </summary>
        protected virtual string RenderFirstLink (int totalPages)
        {
            if (CurrentPage > 1 && totalPages > 0) {
                return $"<li class=\"{ItemCssClass}\"><a class=\"{LinkCssClass}\" href=\"{GetUrl (1)}\">{GetFirstText ()}</a></li>";
            }

            return $"<li class=\"{ItemCssClass} {InactiveItemCssClass}\">" +
            	$"<span class=\"{LinkCssClass}\">{GetFirstText ()}</span></li>";
        }

        /// <summary>
        /// RenderLastLink renders the Last Page link markup.
        /// </summary>
        protected virtual string RenderLastLink (int totalPages)
        {
            if (CurrentPage != totalPages && totalPages > 0) {
                return $"<li class=\"{ItemCssClass}\"><a class=\"{LinkCssClass}\" href=\"{GetUrl (totalPages)}\">{GetLastText ()}</a></li>";
            }

            return $"<li class=\"{ItemCssClass} {InactiveItemCssClass}\">" +
            	$"<span class=\"{LinkCssClass}\">{GetLastText ()}</span></li>";
        }
    }
}

