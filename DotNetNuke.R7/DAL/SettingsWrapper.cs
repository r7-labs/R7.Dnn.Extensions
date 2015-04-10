//
// SettingsWrapper.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014 
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
using System.ComponentModel;
using System.Collections;
using DotNetNuke.UI.Modules;
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.R7
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class SettingsWrapper
    {
        protected ModuleController ctrl;
        protected int ModuleId;
        protected int TabModuleId;
        protected Hashtable settings;

        private SettingsWrapper (int moduleId, int tabModuleId)
        {
            Init (moduleId, tabModuleId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetNuke.Entities.Modules.SettingsWrapper"/> class.
        /// </summary>
        /// <param name='module'>
        /// Module control.
        /// </param>
        public SettingsWrapper (IModuleControl module) : this (module.ModuleContext.ModuleId, module.ModuleContext.TabModuleId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetNuke.Entities.Modules.SettingsWrapper"/> class
        /// and should only be used in generic classes along with <see cref="DotNetNuke.Entities.Modules.SettingsWrapper.Init()" /> call.
        /// </summary>
        public SettingsWrapper ()
        {
        }

        public void Init (int moduleId, int tabModuleId)
        {
            ctrl = new ModuleController ();
            ModuleId = moduleId;
            TabModuleId = tabModuleId;

            // from PortalModuleBase settings definition
            settings = new Hashtable (ctrl.GetModuleSettings (ModuleId));
            var tabModuleSettings = ctrl.GetTabModuleSettings (TabModuleId);

            // combine settings
            foreach (string key in tabModuleSettings.Keys)
                settings [key] = tabModuleSettings [key];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetNuke.Entities.Modules.SettingsWrapper"/> class.
        /// </summary>
        /// <param name='module'>
        /// Module info.
        /// </param>
        public SettingsWrapper (ModuleInfo module) : this (module.ModuleID, module.TabModuleID)
        {
        }

        /// <summary>
        /// Reads module setting.
        /// </summary>
        /// <returns>
        /// The setting value.
        /// </returns>
        /// <param name='settingName'>
        /// Setting name.
        /// </param>
        /// <param name='defaultValue'>
        /// Default value for setting.
        /// </param>
        /// <typeparam name='T'>
        /// Type of the setting
        /// </typeparam>
        protected T ReadSetting<T> (string settingName, T defaultValue)
        {
            T ret;

            if (settings.ContainsKey (settingName))
            {
                var tc = TypeDescriptor.GetConverter (typeof(T));
                try
                {
                    ret = (T)tc.ConvertFrom (settings [settingName]);
                }
                catch
                {
                    ret = defaultValue;
                }
            }
            else
                ret = defaultValue;

            return ret;
        }

        /// <summary>
        /// Writes module or tabmodule setting.
        /// </summary>
        /// <param name='settingName'>
        /// Setting name.
        /// </param>
        /// <param name='value'>
        /// Setting value.
        /// </param>
        /// <param name='tabSpecific'>
        /// If set to <c>true</c>, setting is for this module on current tab.
        /// If set to <c>false</c>, setting is for this module on all tabs.
        /// </param>
        protected void WriteSetting<T> (string settingName, T value, bool tabSpecific)
        {
            if (tabSpecific)
                ctrl.UpdateTabModuleSetting (TabModuleId, settingName, value.ToString ());
            else
                ctrl.UpdateModuleSetting (ModuleId, settingName, value.ToString ());
        }

        /// <summary>
        /// Writes module setting.
        /// </summary>
        /// <param name='settingName'>
        /// Setting name.
        /// </param>
        /// <param name='value'>
        /// Setting value.
        /// </param>
        protected void WriteModuleSetting<T> (string settingName, T value)
        {
            ctrl.UpdateModuleSetting (ModuleId, settingName, value.ToString ());
        }

        /// <summary>
        /// Writes tabmodule setting.
        /// </summary>
        /// <param name='settingName'>
        /// Setting name.
        /// </param>
        /// <param name='value'>
        /// Setting value.
        /// </param>
        protected void WriteTabModuleSetting<T> (string settingName, T value)
        {
            ctrl.UpdateTabModuleSetting (TabModuleId, settingName, value.ToString ());
        }

        /// <summary>
        /// Gets the raw module settings hashtable.
        /// </summary>
        /// <value>The settings.</value>
        public Hashtable Settings 
        {
            get { return settings; }
        }
    }
    // class
}
// namespace

