//
//  EntityConfigurationSample.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2018 Roman M. Yagodin
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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace R7.Dnn.Extensions.Data
{
    public class EntitySample
    {
        public int Key { get; set; }

        public string Value { get; set; }
    }

    public class EntitySampleMapping: IEntityTypeConfiguration<EntitySample>
    {
        public void Configure (EntityTypeBuilder<EntitySample> builder)
        {
            builder.ToTable (DnnTableMappingHelper.GetTableName<EntitySample> ("VendorPrefix", (t) => t.Name.Replace ("Info", string.Empty) + "s"));
            builder.HasKey (m => m.Key);
            builder.Property (m => m.Key).UseSqlServerIdentityColumn ();
            builder.Property (m => m.Value).IsRequired (false);
        }
    }
}