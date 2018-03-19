//
//  ExtensionYamlConfig.cs
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
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace R7.Dnn.Extensions.Core.Configuration
{
    public class ExtensionYamlConfig<TPortalConfig> : ExtensionConfig<TPortalConfig> where TPortalConfig: class, new ()
    {
        public ExtensionYamlConfig (string configFileName, Func<TPortalConfig, TPortalConfig> initCallback = null)
            : base (configFileName, initCallback)
        {
        }

        public override TPortalConfig DeserializeConfig (string configFile)
        {
            using (var configReader = new StringReader (File.ReadAllText (configFile))) {
                var deserializer = new DeserializerBuilder ()
                    .WithNamingConvention (new HyphenatedNamingConvention ())
                    .Build ();

                return deserializer.Deserialize<TPortalConfig> (configReader);
            }
        }
    }
}
