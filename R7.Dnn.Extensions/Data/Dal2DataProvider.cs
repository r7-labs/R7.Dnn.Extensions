//
//  Dal2DataProvider.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016-2018 Roman M. Yagodin
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
using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Collections;

namespace R7.Dnn.Extensions.Data
{
    /// <summary>
    /// Provides interface to access data using DNN DAL2 features.
    /// </summary>
    public class Dal2DataProvider
    {
        /// <summary>
        /// Get single object from the database
        /// </summary>
        /// <returns>
        /// The object
        /// </returns>
        /// <param name='itemId'>
        /// Item identifier.
        /// </param>
        public T Get<T, TKey> (TKey itemId) where T : class
        {
            T item;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                item = repo.GetById (itemId);
            }

            return item;
        }

        /// <summary>
        /// Get single object from the database
        /// </summary>
        /// <returns>
        /// The object
        /// </returns>
        /// <param name='itemId'>
        /// Item identifier.
        /// </param>
        /// <param name='scopeId'>
        /// Scope identifier (like moduleId)
        /// </param>
        /// <typeparam name="T">The type of object to get.</typeparam>
        /// <typeparam name="TKey">The type of the key property of object.</typeparam>
        /// <typeparam name="TScopeKey">The type of the scope property of object.</typeparam>
        public T Get<T, TKey, TScopeKey> (TKey itemId, TScopeKey scopeId) where T : class
        {
            T item;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                item = repo.GetById (itemId, scopeId);
            }

            return item;
        }

        /// <summary>
        /// Gets a single object of type T from result of a dynamic sql query
        /// </summary>
        /// <returns>Object of type T or null.</returns>
        /// <param name="sqlCondition">SQL command condition.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">The type of object to get.</typeparam>
        public T Get<T> (string sqlCondition, params object [] args) where T : class
        {
            T item;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                item = repo.Find (sqlCondition, args).SingleOrDefault ();
            }

            return item;
        }

        /// <summary>
        /// Gets all objects for items matching scopeId
        /// </summary>
        /// <param name='scopeId'>
        /// Scope identifier (like moduleId)
        /// </param>
        /// <returns>Enumerable with objects of type T</returns>
        /// <typeparam name="T">The type of objects to get.</typeparam>
        public IEnumerable<T> GetObjects<T> (int scopeId) where T : class
        {
            IEnumerable<T> items;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                items = repo.Get (scopeId);

                // Without [Scope("ModuleID")] it should be like:
                // items = repo.Find ("WHERE ModuleID = @0", moduleId);
            }

            return items ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets all objects of type T from database
        /// </summary>
        /// <returns>Enumerable with objects of type T</returns>
        /// <typeparam name="T">The type of objects to get.</typeparam>
        public IEnumerable<T> GetObjects<T> () where T : class
        {
            IEnumerable<T> items;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                items = repo.Get ();
            }

            return items ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets the all objects of type T from result of a dynamic sql query
        /// </summary>
        /// <returns>Enumerable with objects of type T</returns>
        /// <param name="sqlCondition">SQL command condition.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">The type of objects to get.</typeparam>
        public IEnumerable<T> GetObjects<T> (string sqlCondition, params object [] args) where T : class
        {
            IEnumerable<T> items;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                items = repo.Find (sqlCondition, args);
            }

            return items ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets the all objects of type T from result of a dynamic sql query
        /// </summary>
        /// <returns>Enumerable with objects of type T</returns>
        /// <param name="cmdType">Type of an SQL command.</param>
        /// <param name="sql">SQL command.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">The type of objects to get.</typeparam>
        public IEnumerable<T> GetObjects<T> (System.Data.CommandType cmdType, string sql, params object [] args) where T : class
        {
            IEnumerable<T> items;

            using (var ctx = DataContext.Instance ()) {
                items = ctx.ExecuteQuery<T> (cmdType, sql, args);
            }

            return items ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets the all objects of type T from result of a stored procedure call
        /// </summary>
        /// <returns>Enumerable with objects of type T</returns>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">The type of objects to get.</typeparam>
        public IEnumerable<T> GetObjectsFromSp<T> (string spName, params object [] args) where T : class
        {
            IEnumerable<T> items;

            using (var ctx = DataContext.Instance ()) {
                items = ctx.ExecuteQuery<T> (System.Data.CommandType.StoredProcedure, spName, args);
            }

            return items ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets stored procedure execution result as scalar value
        /// </summary>
        /// <returns>Stored procedure execution result as scalar value.</returns>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">The type of the scalar.</typeparam>
        public T ExecuteSpScalar<T> (string spName, params object [] args)
        {
            using (var ctx = DataContext.Instance ()) {
                return ctx.ExecuteScalar<T> (System.Data.CommandType.StoredProcedure, spName, args);
            }
        }

        /// <summary>
        /// Finds the objects of type T
        /// </summary>
        /// <returns>Enumerable with objects of type T matching sqlCondition. If searchText is null or whitespace, all objects of type T returned.</returns>
        /// <param name="sqlCondition">SQL condition.</param>
        /// <param name="searchText">Search text.</param>
        /// <param name="dynamicSql">If set to <c>true</c> use dynamic sql arguments with @, otherwize string.Format().</param>
        /// <typeparam name="T">The type of objects to find.</typeparam>
        public IEnumerable<T> FindObjects<T> (string sqlCondition, string searchText, bool dynamicSql = true) where T : class
        {
            return string.IsNullOrWhiteSpace (searchText) ? GetObjects<T> ()
                    : dynamicSql ? GetObjects<T> (sqlCondition, searchText)
                    : GetObjects<T> (string.Format (sqlCondition, searchText));
        }

        /// <summary>
        /// Gets one page of objects of type T
        /// </summary>
        /// <param name="pageIndex">a page index</param>
        /// <param name="pageSize">a page size</param>
        /// <returns>A paged list of T objects</returns>
        /// <typeparam name="T">The type of object to get.</typeparam>
        public IPagedList<T> GetPage<T> (int pageIndex, int pageSize) where T : class
        {
            IPagedList<T> items;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                items = repo.GetPage (pageIndex, pageSize);
            }

            return items ?? new PagedList<T> (Enumerable.Empty<T> (), 0, 0);
        }

        /// <summary>
        /// Gets one page of objects of type T
        /// </summary>
        /// <param name="scopeId">Scope identifier (like moduleId)</param>
        /// <param name="pageIndex">a page index</param>
        /// <param name="pageSize">a page size</param>
        /// <returns>A paged list of T objects</returns>
        /// <typeparam name="T">The type of object to get.</typeparam>
        /// <typeparam name="TScopeKey">The type of the scope property of object.</typeparam>
        public IPagedList<T> GetPage<T, TScopeKey> (TScopeKey scopeId, int pageIndex, int pageSize) where T : class
        {
            IPagedList<T> items;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                items = repo.GetPage (scopeId, pageIndex, pageSize);
            }

            return items ?? new PagedList<T> (Enumerable.Empty<T> (), 0, 0);
        }

        /// <summary>
        /// Gets one page of objects of type T
        /// </summary>
        /// <param name="sqlCondition">SQL condition.</param>
        /// <param name="pageIndex">a page index</param>
        /// <param name="pageSize">a page size</param>
        /// <param name="args">SQL command arguments.</param>
        /// <returns>A paged list of T objects</returns>
        /// <typeparam name="T">The type of objects to get.</typeparam>
        public IPagedList<T> GetPage<T> (string sqlCondition, int pageIndex, int pageSize, params object [] args) where T : class
        {
            IPagedList<T> items;

            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                items = repo.Find (pageIndex, pageSize, sqlCondition, args);
            }

            return items ?? new PagedList<T> (Enumerable.Empty<T> (), 0, 0);
        }

        /// <summary>
        /// Adds a new T object into the database
        /// </summary>
        /// <param name='item'></param>
        /// <typeparam name="T">The type of object to add.</typeparam>
        public void Add<T> (T item) where T : class
        {
            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                repo.Insert (item);
            }
        }

        /// <summary>
        /// Updates an object already stored in the database
        /// </summary>
        /// <param name='item'>
        /// item.
        /// </param>
        /// <typeparam name="T">The type of object to update.</typeparam>
        public void Update<T> (T item) where T : class
        {
            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                repo.Update (item);
            }
        }

        /// <summary>
        /// Delete a given item from the database by instance
        /// </summary>
        /// <param name='item'></param>
        /// <typeparam name="T">The type of object to delete.</typeparam>
        public void Delete<T> (T item) where T : class
        {
            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                repo.Delete (item);
            }
        }

        /// <summary>
        /// Delete a given item from the database by ID
        /// </summary>
        /// <param name='itemId'></param>
        /// <typeparam name="T">The type of object to delete.</typeparam>
        /// <typeparam name="TKey">The type of the key property of object.</typeparam>
        public void Delete<T, TKey> (TKey itemId) where T : class
        {
            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                repo.Delete (repo.GetById (itemId));
            }
        }

        /// <summary>
        /// Delete some item from the database using SQL condition
        /// </summary>
        /// <param name='sqlCondition'>SQL condition</param>
        /// <param name='args'>Optional arguments</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public void Delete<T> (string sqlCondition, params object [] args) where T : class
        {
            using (var ctx = DataContext.Instance ()) {
                var repo = ctx.GetRepository<T> ();
                repo.Delete (sqlCondition, args);
            }
        }
    }
}

