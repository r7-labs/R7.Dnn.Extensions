//
// ViewModelContext_TSettings.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2016 Roman M. Yagodin
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
using System.Web.UI;
using DotNetNuke.UI.Modules;

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

