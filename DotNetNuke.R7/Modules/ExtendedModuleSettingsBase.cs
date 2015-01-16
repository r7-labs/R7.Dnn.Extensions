//
// ExtendedModuleSettingsBase.cs
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

namespace DotNetNuke.Entities.Modules
{
    public class ExtendedModuleSettingsBase <TC, TS> : ModuleSettingsBase
        where TC : ControllerBase, new ()
        where TS : SettingsWrapper, new ()
    {
        private TC ctrl;

        private TS settings;

        protected TC Controller
        {
            get {  { return ctrl ?? (ctrl = new TC ()); } }
        }

        /// <summary>
        /// Gets the module settings. Raw settings hashtable is accessible via 
        /// <see cref="DotNetNuke.Entities.Modules.SettingsWrapper.Settings" /> property.
        /// </summary>
        /// <value>The module settings.</value>
        protected new TS Settings
        {
            get 
            { 
                if (settings == null)
                {
                    settings = new TS ();
                    settings.Init (ModuleId, TabModuleId);
                }

                return settings; 
            }
        }
    }
}

