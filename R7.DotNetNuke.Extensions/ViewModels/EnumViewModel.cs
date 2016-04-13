//
//  EnumViewModel.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2016 Roman M. Yagodin
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
using System.Collections.Generic;
using DotNetNuke.Services.Localization;

namespace R7.DotNetNuke.Extensions.ViewModels
{
    public class EnumViewModel<T> where T: struct
    {
        #region Protected members

        protected ViewModelContext Context { get; set; }

        #endregion

        public EnumViewModel (T? value)
        {
            // where T: enum
            if (!typeof (T).IsEnum)
            {
                throw new NotSupportedException ("Type parameter of EnumViewModel must be enum.");
            }

            Value = value;
        }

        #region Public properties

        public T? Value { get; protected set; }

        public string ValueLocalized
        {
            get { return Localization.GetString (ValueResourceKey, Context.LocalResourceFile); }
        }

        public string ValueResourceKey
        {
            get { return GetValueResourceKey (Value); } 
        }

        #endregion

        #region Static members

        public static List<EnumViewModel<T>> GetValues (ViewModelContext context, bool includeDefault)
        {
            var values = new List<EnumViewModel<T>> ();

            if (includeDefault)
            {
                var v1 = new EnumViewModel<T> (null);
                v1.Context = context;
                values.Add (v1);
            }

            foreach (T value in Enum.GetValues (typeof (T)))
            {   
                var v1 = new EnumViewModel<T> (value);
                v1.Context = context;
                values.Add (v1);
            }

            return values;
        }

        public static string GetValueResourceKey (T? value)
        {
            if (value != null)
            {
                return typeof (T).Name + "_" + value.Value + ".Text";
            }

            return typeof (T).Name + "_Default.Text";
        }

        #endregion
    }
}
