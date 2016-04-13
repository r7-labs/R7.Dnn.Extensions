//
//  ExtendedPortalModuleBase.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2015, 2016 Roman M. Yagodin
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
using DotNetNuke.Entities.Modules;
using R7.DotNetNuke.Extensions.Entities.Modules;

namespace R7.DotNetNuke.Extensions
{
    [Obsolete ("Use R7.DotNetNuke.Extensions.Entities.Modules.PortalModuleBase{TSettings} class")]
    public class ExtendedPortalModuleBase <TC, TS> : PortalModuleBase 
        where TC : ControllerBase, new ()
        where TS : SettingsWrapper, new ()
    {
        private TC ctrl;

        private TS settings;

        [Obsolete ("Use instance of R7.DotNetNuke.Extensions.Dal2DataProvider instead")]
        protected TC Controller
        {
            get {  { return ctrl ?? (ctrl = new TC ()); } }
        }

        /// <summary>
        /// Gets the module settings. Raw settings hashtable is accessible via 
        /// <see cref="R7.DotNetNuke.Extensions.SettingsWrapper.Settings" /> property.
        /// </summary>
        /// <value>The module settings.</value>
        protected new TS Settings
        {
            get 
            { 
                if (settings == null)
                {
                    settings = new TS ();
                    settings.Init (ModuleId, TabModuleId, PortalId);
                }

                return settings; 
            }
        }
    }
}

