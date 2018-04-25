//
//  EfCoreDataContextBase.cs
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

using System.Linq;
using DotNetNuke.Common.Utilities;
using Microsoft.EntityFrameworkCore;

namespace R7.Dnn.Extensions.Data
{
    public abstract class EfCoreDnnDataContextBase: EfCoreDataContextBase, IDataContext
    {
        protected EfCoreDnnDataContextBase ()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer (Config.GetConnectionString ("SiteSqlServer"));
            }
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            var databaseOwner = Config.GetDataBaseOwner ();
            // remove trailing '.' from schema name, by ex. "dbo." => "dbo"
            modelBuilder.HasDefaultSchema (databaseOwner.Substring (0, databaseOwner.Length - 1));

            // add model configurations mappings (in child classes)
            // modelBuilder.ApplyConfiguration (new EntitySampleMapping ());
        }

        #region IDataContext implementation

        public override IQueryable<TEntity> FromSql<TEntity> (string sql, params object [] parameters)
        {
            return Set<TEntity> ().FromSql (sql.Replace ("{objectQualifier}", Config.GetObjectQualifer ()), parameters).AsNoTracking ();
        }

        #endregion
    }
}
