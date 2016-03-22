//
//  ControllerBase.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014, 2015, 2016 Roman M. Yagodin
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

