//
//  ViewModelContext_TSettings.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016 Roman M. Yagodin
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
using System.Web.UI;
using DotNetNuke.UI.Modules;
using DotNetNuke.R7.Entities.Modules;

namespace DotNetNuke.R7.ViewModels
{
    public class ViewModelContext<TSettings>: ViewModelContext
        where TSettings: SettingsWrapper, new ()
    {
        public ViewModelContext (IModuleControl module): base (module)
        {}

        public ViewModelContext (Control control, IModuleControl module): base (control, module)
        {}

        public ViewModelContext (IModuleControl module, TSettings settings): base (module)
        {
            this.settings = settings;
        }

        public ViewModelContext (Control control, IModuleControl module, TSettings settings): base (control, module)
        {
            this.settings = settings;
        }

       TSettings settings;
        public TSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = new TSettings ();
                    settings.Init (Module.ModuleId, Module.TabModuleId);
                }

                return settings;
            }
        }
    }
}

