//
//  IDataContext.cs
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

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using R7.Dnn.Extensions.Models;

namespace R7.Dnn.Extensions.Data
{
    public interface IDataContext: IDisposable
    {
        DbSet<TEntity> Set<TEntity> () where TEntity : class;

        IQueryable<TEntity> FromSql<TEntity> (string queryName, params object [] parameters) where TEntity : class;

        void WasModified<TEntity> (TEntity entity) where TEntity : class;

        void WasRemoved<TEntity> (TEntity entity) where TEntity : class;

        int SaveChanges ();

        ITransaction BeginTransaction ();
    }
}
