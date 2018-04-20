//
//  ExtensionYamlConfigTests.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2017 Roman M. Yagodin
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.IO;
using R7.Dnn.Extensions.Configuration;
using Xunit;

namespace R7.Dnn.Extensions.Tests.Configuration
{
    public class TestPortalConfig
    {
        public int Key { get; set; } = 1;

        public string Value { get; set; } = "Default value";
    }

    public class ExtensionYamlConfigTests
    {
        [Fact]
        public void ReadConfigTest ()
        {
            var config = new ExtensionYamlConfig<TestPortalConfig> (
                Path.GetFullPath ("../../Configuration/Config.yml"), cfg => {
                    return cfg;
                }
            );

            Assert.Equal (10, config.GetInstance (0).Key);
            Assert.Equal ("Value from config", config.GetInstance (0).Value);
        }

        [Fact]
        public void DefaultConfigTest ()
        {
            var config = new ExtensionYamlConfig<TestPortalConfig> (
                Path.GetFullPath ("SomeNonExistentConfigFile.yml"), cfg => {
                    return cfg;
                }
            );

            Assert.Equal (1, config.GetInstance (0).Key);
            Assert.Equal ("Default value", config.GetInstance (0).Value);
        }
    }
}
