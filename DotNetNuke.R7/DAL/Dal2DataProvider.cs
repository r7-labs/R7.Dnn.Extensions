//
// Dal2DataProvider.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2016
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Data;
using DotNetNuke.Collections;

namespace DotNetNuke.R7
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
        public T Get<T> (int itemId) where T: class
        {
            T info;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                info = repo.GetById (itemId);
            }

            return info;
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
        public T Get<T> (int itemId, int scopeId) where T: class
        {
            T info;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                info = repo.GetById (itemId, scopeId);
            }

            return info;
        }

        /// <summary>
        /// Gets a single object of type T from result of a dynamic sql query
        /// </summary>
        /// <returns>Object of type T or null.</returns>
        /// <param name="sqlCondition">SQL command condition.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        public T Get<T> (string sqlCondition, params object [] args) where T: class
        {
            T info;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                info = repo.Find (sqlCondition, args).SingleOrDefault ();
            }

            return info;
        }

        /// <summary>
        /// Gets all objects for items matching scopeId
        /// </summary>
        /// <param name='scopeId'>
        /// Scope identifier (like moduleId)
        /// </param>
        /// <returns></returns>
        public IEnumerable<T> GetObjects<T> (int scopeId) where T: class
        {
            IEnumerable<T> infos;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                infos = repo.Get (scopeId);

                // Without [Scope("ModuleID")] it should be like:
                // infos = repo.Find ("WHERE ModuleID = @0", moduleId);
            }

            return infos ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets all objects of type T from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetObjects<T> () where T: class
        {
            IEnumerable<T> infos;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                infos = repo.Get ();
            }

            return infos ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets the all objects of type T from result of a dynamic sql query
        /// </summary>
        /// <returns>Enumerable with objects of type T</returns>
        /// <param name="sqlCondition">SQL command condition.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        public IEnumerable<T> GetObjects<T> (string sqlCondition, params object [] args) where T: class
        {
            IEnumerable<T> infos;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                infos = repo.Find (sqlCondition, args);
            }

            return infos ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets the all objects of type T from result of a dynamic sql query
        /// </summary>
        /// <returns>Enumerable with objects of type T</returns>
        /// <param name="cmdType">Type of an SQL command.</param>
        /// <param name="sql">SQL command.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        public IEnumerable<T> GetObjects<T> (System.Data.CommandType cmdType, string sql, params object [] args) where T: class
        {
            IEnumerable<T> infos;

            using (var ctx = DataContext.Instance ())
            {
                infos = ctx.ExecuteQuery<T> (cmdType, sql, args);
            }

            return infos ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Gets the all objects of type T from result of a stored procedure call
        /// </summary>
        /// <returns>Enumerable with objects of type T</returns>
        /// <param name="spName">Stored procedure name.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        public IEnumerable<T> GetObjectsFromSp<T> (string spName, params object [] args) where T: class
        {
            IEnumerable<T> infos;

            using (var ctx = DataContext.Instance ())
            {
                infos = ctx.ExecuteQuery<T> (System.Data.CommandType.StoredProcedure, spName, args);
            }

            return infos ?? Enumerable.Empty<T> ();
        }

        /// <summary>
        /// Finds the objects of type T
        /// </summary>
        /// <returns>Enumerable with objects of type T matching sqlCondition. If searchText is null or whitespace, all objects of type T returned.</returns>
        /// <param name="sqlCondition">SQL condition.</param>
        /// <param name="searchText">Search text.</param>
        /// <param name="dynamicSql">If set to <c>true</c> use dynamic sql arguments with @, otherwize string.Format().</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        public IEnumerable<T> FindObjects<T> (string sqlCondition, string searchText, bool dynamicSql = true) where T: class
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
        public IPagedList<T> GetPage<T> (int pageIndex, int pageSize) where T: class
        {
            IPagedList<T> infos;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                infos = repo.GetPage (pageIndex, pageSize);
            }

            return infos ?? new PagedList<T> (Enumerable.Empty<T> (), 0, 0);
        }

        /// <summary>
        /// Gets one page of objects of type T
        /// </summary>
        /// <param name="scopeId">Scope identifier (like moduleId)</param>
        /// <param name="pageIndex">a page index</param>
        /// <param name="pageSize">a page size</param>
        /// <returns>A paged list of T objects</returns>
        public IPagedList<T> GetPage<T> (int scopeId, int pageIndex, int pageSize) where T: class
        {
            IPagedList<T> infos;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                infos = repo.GetPage (scopeId, pageIndex, pageSize);
            }

            return infos ?? new PagedList<T> (Enumerable.Empty<T> (), 0, 0);
        }

        /// <summary>
        /// Gets one page of objects of type T
        /// </summary>
        /// <param name="sqlCondition">SQL condition.</param>
        /// <param name="pageIndex">a page index</param>
        /// <param name="pageSize">a page size</param>
        /// <param name="args">SQL command arguments.</param>
        /// <returns>A paged list of T objects</returns>
        public IPagedList<T> GetPage<T> (string sqlCondition, int pageIndex, int pageSize, params object [] args) where T: class
        {
            IPagedList<T> infos;

            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                infos = repo.Find (pageIndex, pageSize, sqlCondition, args);
            }

            return infos ?? new PagedList<T> (Enumerable.Empty<T> (), 0, 0);
        }

        /// <summary>
        /// Adds a new T object into the database
        /// </summary>
        /// <param name='info'></param>
        public void Add<T> (T info) where T: class
        {
            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                repo.Insert (info);
            }
        }

        /// <summary>
        /// Updates an object already stored in the database
        /// </summary>
        /// <param name='info'>
        /// Info.
        /// </param>
        public void Update<T> (T info) where T: class
        {
            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                repo.Update (info);
            }
        }

        /// <summary>
        /// Delete a given item from the database by instance
        /// </summary>
        /// <param name='info'></param>
        public void Delete<T> (T info) where T: class
        {
            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                repo.Delete (info);
            }
        }

        /// <summary>
        /// Delete a given item from the database by ID
        /// </summary>
        /// <param name='itemId'></param>
        public void Delete<T> (int itemId) where T: class
        {
            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                repo.Delete (repo.GetById (itemId));
            }
        }

        /// <summary>
        /// Delete some item from the database using SQL condition
        /// </summary>
        /// <param name='sqlCondition'>SQL condition</param>
        /// <param name='args'>Optional arguments</param>
        public void Delete<T> (string sqlCondition, params object [] args) where T: class
        {
            using (var ctx = DataContext.Instance ())
            {
                var repo = ctx.GetRepository<T> ();
                repo.Delete (sqlCondition, args);
            }
        }
    }
}

