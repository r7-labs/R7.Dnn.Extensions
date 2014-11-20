//
// ListControlExtensions.cs
//
// Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
// Copyright (c) 2014 
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
using System.Web.UI.WebControls;

namespace DotNetNuke.R7
{
    public static class ListControlExtensions
    {
        /// <summary>
        /// Finds the item index by it's value in ListControl-type list.
        /// </summary>
        /// <returns>Item index.</returns>
        /// <param name="list">List control.</param>
        /// <param name="value">A value.</param>
        /// <param name="defaultIndex">Default index (in case item not found).</param>
        public static int FindIndexByValue (this ListControl list, object value, int defaultIndex = 0)
        { 
            if (value != null)
            {
                var index = 0;
                var strvalue = value.ToString ();
                foreach (ListItem item in list.Items)
                {
                    if (item.Value == strvalue)
                        return index;
                    index++;
                }
            }

            return defaultIndex; 
        }

        /// <summary>
        /// Sets the selected index of ListControl-type list.
        /// </summary>
        /// <param name="list">List control.</param>
        /// <param name="value">A value.</param>
        /// <param name="defaultIndex">Default index (in case item not found).</param>
        public static void SelectByValue (this ListControl list, object value, int defaultIndex = 0)
        {
            list.SelectedIndex = FindIndexByValue (list, value, defaultIndex);
        }
    }
}

