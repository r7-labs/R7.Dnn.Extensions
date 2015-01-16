//
// ModuleExtensions.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2015
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

