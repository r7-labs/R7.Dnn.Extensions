//
//  Dal2CrudProvider.cs
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

using R7.Dnn.Extensions.Models;

namespace R7.Dnn.Extensions.Data
{
    public class Dal2CrudProvider<T>: ICrudProvider<T> where T : class
    {
        protected readonly Dal2DataProvider DataProvider;

        public Dal2CrudProvider (Dal2DataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        public T Get<TKey> (TKey itemId)
        {
            return DataProvider.Get<T, TKey> (itemId);
        }

        public void Add (T item)
        {
            DataProvider.Add (item);
        }

        public void Update (T item)
        {
            DataProvider.Update (item);
        }

        public void Delete (T item)
        {
            DataProvider.Delete (item);
        }
    }
}
