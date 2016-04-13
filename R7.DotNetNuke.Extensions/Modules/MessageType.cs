//
//  MessageType.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014, 2015 Roman M. Yagodin
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
using DotNetNuke.UI.Skins.Controls;

namespace R7.DotNetNuke.Extensions
{
    /// <summary>
    /// Module message types.
    /// </summary>
    public enum MessageType
    {
        // duplicate ModuleMessage.ModuleMessageType values here
        Success = ModuleMessage.ModuleMessageType.GreenSuccess,
        Info = ModuleMessage.ModuleMessageType.BlueInfo,
        Warning = ModuleMessage.ModuleMessageType.YellowWarning,
        Error = ModuleMessage.ModuleMessageType.RedError
    }
}

