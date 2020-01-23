//
//  ContentHelper.cs
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

using System.Linq;
using DotNetNuke.Entities.Content;

namespace R7.Dnn.Extensions.Content
{
    public static class ContentHelper
    {
        public static IQueryable<ContentItem> GetContentItemsByFile (int fileId)
        {
            return GetContentItemsByFile (fileId, new ContentController ());
        }

        public static IQueryable<ContentItem> GetContentItemsByFile (int fileId, ContentController contentCtrl)
        {
            return contentCtrl.GetUnIndexedContentItems ().Where (ci =>
                ci.Files.Any (ci2 => ci2.FileId == fileId) ||
                ci.Images.Any (ci2 => ci2.FileId == fileId) ||
                ci.Videos.Any (ci2 => ci2.FileId == fileId));
        }
    }
}
