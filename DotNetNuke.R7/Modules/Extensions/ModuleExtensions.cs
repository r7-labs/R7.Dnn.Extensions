//
//  ModuleExtensions.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014,2015 Roman M. Yagodin
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
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.R7
{
    public static class ModuleExtensions
    {
        /// <summary>
        /// Displays a message of messageType for specified module with heading, with optional localization.
        /// </summary>
        /// <param name="module">Module.</param>
        /// <param name="heading">Message heading.</param>
        /// <param name="message">Message body.</param>
        /// <param name="messageType">Message type.</param>
        /// <param name="localize">If set to <c>true</c> localize message and heading.</param>
        public static void Message (this PortalModuleBase module, string heading, string message, MessageType messageType = MessageType.Info, bool localize = false)
        {
            var locheading = localize ? Localization.GetString (heading, module.LocalResourceFile) : heading;
            var locmessage = localize ? Localization.GetString (message, module.LocalResourceFile) : message;
            Skin.AddModuleMessage (module, locheading, locmessage,
                (ModuleMessage.ModuleMessageType) messageType);
        }

        /// <summary>
        /// Displays a message of messageType for specified module, with optional localization.
        /// </summary>
        /// <param name="module">Module.</param>
        /// <param name="message">Message body.</param>
        /// <param name="messageType">Message type.</param>
        /// <param name="localize">If set to <c>true</c> localize message.</param>
        public static void Message (this PortalModuleBase module, string message, MessageType messageType = MessageType.Info, bool localize = false)
        {
            var locmessage = localize ? Localization.GetString (message, module.LocalResourceFile) : message;
            Skin.AddModuleMessage (module, locmessage,
                (ModuleMessage.ModuleMessageType) messageType);
        }

    }
}

