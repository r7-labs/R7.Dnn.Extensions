//
//  ExtensionConfig.cs
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

using System;
using System.Collections.Concurrent;
using System.IO;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;

namespace R7.Dnn.Extensions.Configuration
{
    public abstract class ExtensionConfig<TPortalConfig> where TPortalConfig : class, new()
    {
        readonly ConcurrentDictionary<int, Lazy<TPortalConfig>> _portalConfigs =
            new ConcurrentDictionary<int, Lazy<TPortalConfig>> ();

        readonly string _configFileName;

        readonly Func<TPortalConfig,TPortalConfig> _initCallback;

        protected ExtensionConfig (string configFileName, Func<TPortalConfig, TPortalConfig> initCallback = null)
        {
            _configFileName = configFileName;
            _initCallback = initCallback;
        }

        public TPortalConfig Instance {
            get { return GetInstance (PortalSettings.Current.PortalId); }
        }

        public TPortalConfig GetInstance (int portalId)
        {
            return _portalConfigs.GetOrAdd (portalId, portalId2 =>
                new Lazy<TPortalConfig> (() => {
                    return InitConfig (GetPortalConfig (portalId2));
                }                              
            )).Value;
        }

        public virtual TPortalConfig InitConfig (TPortalConfig config)
        {
            return (_initCallback != null)? _initCallback (config) : config;
        }

        TPortalConfig GetPortalConfig (int portalId)
        {
            var portalConfigFile = Path.IsPathRooted (_configFileName)
                                       ? _configFileName
                                       : Path.Combine (new PortalSettings (portalId).HomeDirectoryMapPath, _configFileName);

            try {
                return (File.Exists (portalConfigFile))
                    ? DeserializeConfig (portalConfigFile)
                    : new TPortalConfig ();
            }
            catch (Exception ex)
            {
                Exceptions.LogException (new Exception ($"Cannot deserialize the \"{portalConfigFile}\" config file", ex));
                return new TPortalConfig ();
            }
        }

        public abstract TPortalConfig DeserializeConfig (string configFile);
    }
}
