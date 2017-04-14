//
//  IModuleControlWithSettings.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
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

using DotNetNuke.Entities.Modules.Settings;
using DotNetNuke.UI.Modules;

namespace R7.Dnn.Extensions.Modules
{
    /// <summary>
    /// Module control with settings.
    /// </summary>
    public interface IModuleControlWithSettings<TSettings>: IModuleControl
        where TSettings : class, new()
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        TSettings Settings { get; }

        /// <summary>
        /// Gets or sets the settings repository.
        /// </summary>
        /// <value>The settings repository.</value>
        SettingsRepository<TSettings> SettingsRepository { get; set; }

        /// <summary>
        /// Creates the settings repository.
        /// </summary>
        /// <returns>The settings repository.</returns>
        SettingsRepository<TSettings> CreateSettingsRepository ();
    }
}
