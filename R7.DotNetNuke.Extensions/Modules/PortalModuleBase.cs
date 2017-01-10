//
//  PortalModuleBase.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016-2017 Roman M. Yagodin
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

using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Settings;

namespace R7.DotNetNuke.Extensions.Modules
{
    /// <summary>
    /// Base class for module view and edit controls, extended with strongly-typed settings.
    /// </summary>
    public abstract class PortalModuleBase<TSettings>: PortalModuleBase, 
        IModuleControlWithSettings<TSettings> where TSettings : class, new ()
    {
        #region Private fields

        TSettings settings;

        SettingsRepository<TSettings> settingsRepo;

        #endregion

        /// <summary>
        /// Gets strongly-typed module settings.
        /// </summary>
        /// <value>The module settings.</value>
        public new TSettings Settings {
            get { return settings; }
        }

        /// <summary>
        /// Gets or sets the settings repository.
        /// </summary>
        /// <value>The settings repository.</value>
        public SettingsRepository<TSettings> SettingsRepository {
            get {
                return settingsRepo ?? (settingsRepo = CreateSettingsRepository ());
            }
            set {
                settingsRepo = value;
                settings = SettingsRepository.GetSettings (ModuleContext.Configuration);
            }
        }

        /// <summary>
        /// Creates the settings repository.
        /// </summary>
        /// <returns>The settings repository.</returns>
        public virtual SettingsRepository<TSettings> CreateSettingsRepository ()
        {
            return new SettingsRepositoryImpl<TSettings> ();
        }
    }
}

