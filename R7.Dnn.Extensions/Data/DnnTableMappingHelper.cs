//
//  DnnTableMappingHelper.cs
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
using DotNetNuke.Common.Utilities;

namespace R7.Dnn.Extensions.Data
{
    // TODO: Extract ITableMappingHelper?
    public static class DnnTableMappingHelper
    {
        public static string GetTableName<T> (string vendorPrefix, Func<Type, string> getTableName = null) where T : class
        {
            var tableName = (getTableName != null) ? getTableName (typeof (T)) : typeof (T).Name;
            return $"{Config.GetObjectQualifer ()}{vendorPrefix}_{tableName}";
        }
    }
}