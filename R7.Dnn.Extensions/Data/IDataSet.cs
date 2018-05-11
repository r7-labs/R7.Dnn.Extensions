//
//  IDataSet.cs
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

namespace R7.Dnn.Extensions.Data
{
    public interface IDataSet<TEntity> where TEntity: class
    {
        IQueryable<TEntity> Query ();

        IQueryable<TEntity> FromSql (string queryName, params object [] parameters);

        TEntity Find<TKey> (TKey key);

        bool Exists (TEntity entity);

        void Add (TEntity entity);

        void Attach (TEntity entity);

        void Remove (TEntity entity);
    }
}
