//
// ControllerBase.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014-2016
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
using DotNetNuke.Entities.Modules;

namespace DotNetNuke.R7
{
    [Obsolete("Use Dal2DataProvider to access data and ModuleSearchBase to inherit module controllers")]
    public abstract class ControllerBase : ModuleSearchBase
    {
        protected readonly Dal2DataProvider dataProvider = new Dal2DataProvider ();

        #region Common methods

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetNuke.R7.ControllerBase"/> class.
        /// </summary>
        protected ControllerBase ()
        { 
        }

        /// <summary>
        /// Adds a new T object into the database
        /// </summary>
        /// <param name='info'></param>
        public void Add<T> (T info) where T: class
        {
            dataProvider.Add (info);
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
        public T Get<T> (int itemId) where T: class
        {
            return dataProvider.Get<T> (itemId);
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
            return dataProvider.Get<T> (itemId, scopeId);
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
            return dataProvider.Get<T> (sqlCondition, args);
        }

        /// <summary>
        /// Updates an object already stored in the database
        /// </summary>
        /// <param name='info'>
        /// Info.
        /// </param>
        public void Update<T> (T info) where T: class
        {
            dataProvider.Update (info);
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
            return dataProvider.GetObjects<T> (scopeId);
        }

        /// <summary>
        /// Gets all objects of type T from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetObjects<T> () where T: class
        {
            return dataProvider.GetObjects<T> ();
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
            return dataProvider.GetObjects<T> (sqlCondition, args);
        }

        /// <summary>
        /// Gets the all objects of type T from result of a dynamic sql query
        /// </summary>
        /// <returns>Enumerable with objects of type T</returns>
        /// <param name="cmdType">Type of an SQL command.</param>
        /// <param name="sql">SQL command.</param>
        /// <param name="args">SQL command arguments.</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        public IEnumerable<T> GetObjects<T> (System.Data.CommandType cmdType, string sql, params object[] args) where T: class
        {
            return dataProvider.GetObjects<T> (cmdType, sql, args);
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
            return dataProvider.GetObjectsFromSp<T> (spName, args);
        }

        /// <summary>
        /// Finds the objects of type T
        /// </summary>
        /// <returns>Enumerable with objects of type T matching sqlCondition. If searchText is null or whitespace, all objects of type T returned.</returns>
        /// <param name="sqlCondition">SQL conditon.</param>
        /// <param name="searchText">Search text.</param>
        /// <param name="dynamicSql">If set to <c>true</c> use dynamic sql arguments with @, otherwize string.Format().</param>
        /// <typeparam name="T">Type of objects.</typeparam>
        public IEnumerable<T> FindObjects<T> (string sqlCondition, string searchText, bool dynamicSql = true) where T: class
        {
            return dataProvider.FindObjects<T> (sqlCondition, searchText, dynamicSql);
        }

        /// <summary>
        /// Gets one page of objects of type T
        /// </summary>
        /// <param name="scopeId">Scope identifier (like moduleId)</param>
        /// <param name="index">a page index</param>
        /// <param name="size">a page size</param>
        /// <returns>A paged list of T objects</returns>
        public IPagedList<T> GetPage<T> (int scopeId, int index, int size) where T: class
        {
            return dataProvider.GetPage<T> (scopeId, index, size);
        }

        /// <summary>
        /// Gets one page of objects of type T
        /// </summary>
        /// <param name="index">a page index</param>
        /// <param name="size">a page size</param>
        /// <returns>A paged list of T objects</returns>
        public IPagedList<T> GetPage<T> (int index, int size) where T: class
        {
            return dataProvider.GetPage<T> (index, size);
        }

        /// <summary>
        /// Delete a given item from the database by instance
        /// </summary>
        /// <param name='info'></param>
        public void Delete<T> (T info) where T: class
        {
            dataProvider.Delete (info);
        }

        /// <summary>
        /// Delete a given item from the database by ID
        /// </summary>
        /// <param name='itemId'></param>
        public void Delete<T> (int itemId) where T: class
        {
            dataProvider.Delete<T> (itemId);
        }

        /// <summary>
        /// Delete some item from the database using SQL condition
        /// </summary>
        /// <param name='sqlCondition'>SQL condition</param>
        /// <param name='args'>Optional arguments</param>
        public void Delete<T> (string sqlCondition, params object [] args) where T: class
        {
            dataProvider.Delete<T> (sqlCondition, args);
        }

        #endregion
    }
}

