﻿//
//  ModelContextBase.cs
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
using System.Linq.Expressions;
using R7.Dnn.Extensions.Data;

namespace R7.Dnn.Extensions.Models
{
    /// <summary>
    /// Generic repository and unit of work wrapper for IDataContext
    /// </summary>
    public abstract class ModelContextBase: IModelContext
    {
        private bool _disposed = false;

        // TODO: Use factory, not data context?
        private IDataContext _context;
        protected IDataContext Context {
            get {
                if (!_disposed) {
                    return _context ?? (_context = CreateDataContext ());
                }

                throw new InvalidOperationException ("Cannot use repository after it was disposed.");
            }
        }

        public abstract IDataContext CreateDataContext ();

        protected ModelContextBase ()
        {
        }

        protected ModelContextBase (IDataContext dbContext)
        {
            _context = dbContext;
        }

        #region IModelRepository implementation

        public virtual IQueryable<TEntity> Query<TEntity> () where TEntity : class
        {
            return Context.Set<TEntity> ();
        }

        public virtual IQueryable<TEntity> QueryOne<TEntity> (Expression<Func<TEntity, bool>> keySelector) where TEntity : class
        {
            if (keySelector == null) {
                throw new ArgumentException ("Key selector cannot be null.");
            }

            return Context.Set<TEntity> ().Where (keySelector).Take (1);
        }

        public virtual IQueryable<TEntity> Query<TEntity> (string sql, params object [] parameters) where TEntity : class
        {
            return Context.FromSql<TEntity> (sql, parameters);
        }

        public virtual TEntity Get<TEntity> (object key) where TEntity : class
        {
            if (key == null) {
                throw new ArgumentException ("Key value cannot be null.");
            }

            return Context.Set<TEntity> ().Find (key);
        }

        public virtual void Add<TEntity> (TEntity entity) where TEntity : class
        {
            Context.Set<TEntity> ().Add (entity);
        }

        public virtual void Update<TEntity> (TEntity entity) where TEntity : class
        {
            Context.WasModified (entity);
        }

        // TODO: Test UpdateExternal
        public virtual void UpdateExternal<TEntity> (TEntity entity) where TEntity : class
        {
            Context.Set<TEntity> ().Attach (entity);
            Context.WasModified (entity);
        }

        public virtual void Remove<TEntity> (TEntity entity) where TEntity : class
        {
            Context.Set<TEntity> ().Remove (entity);
        }

        public virtual void RemoveExternal<TEntity> (TEntity entity) where TEntity : class
        {
            Context.Set<TEntity> ().Attach (entity);
            Context.WasRemoved (entity);
        }

        public bool Exists<TEntity> (TEntity entity) where TEntity : class
        {
            return Context.Set<TEntity> ().Local.Any (e => e == entity);
        }

        // TODO: Combine SaveChanges (true) with transaction commit?
        public virtual bool SaveChanges (bool dispose = true)
        {
            var result = Context.SaveChanges () > 0;

            if (dispose) {
                Dispose ();
            }

            return result;
        }

        public virtual ITransaction BeginTransaction ()
        {
            return Context.BeginTransaction ();
        }

        #endregion

        #region IDisposable implementation

        public void Dispose ()
        {
            _disposed = true;
            if (_context != null) {
                _context.Dispose ();
            }
        }

        #endregion
    }
}