//
//  DnnTreeViewExtensions.cs
//
//  Author:
//       Roman M. Yagodin <roman.yagodin@gmail.com>
//
//  Copyright (c) 2014-2016 Roman M. Yagodin
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
using DotNetNuke.Web.UI.WebControls;

namespace R7.Dnn.Extensions.Controls
{
    /// <summary>
    /// DnnTreeView extensions.
    /// </summary>
    public static class DnnTreeViewExtensions
    {
        /// <summary>
        /// Expands node with specified value and all it's parent nodes
        /// </summary>
        /// <param name="treeview">DNN treeview.</param>
        /// <param name="value">Value of the node.</param>
        /// <param name="ignoreCase">If set to <c>true</c> ignore value case.</param>
        public static void SelectAndExpandByValue (this DnnTreeView treeview, string value, bool ignoreCase = false)
        {
            if (!string.IsNullOrWhiteSpace (value))
            {
                var treeNode = treeview.FindNodeByValue (value, ignoreCase);
                if (treeNode != null)
                {
                    treeNode.Selected = true;

                    // expand all parent nodes
                    treeNode = treeNode.ParentNode;
                    while (treeNode != null)
                    {
                        treeNode.Expanded = true;
                        treeNode = treeNode.ParentNode;
                    } 
                }
            }
        }

        /// <summary>
        /// Inserts the default node at the root of the tree.
        /// </summary>
        /// <param name="treeView">Tree control.</param>
        /// <param name="text">Default node text.</param>
        /// <param name="value">Default node value.</param>
        public static void InsertDefaultNode (this DnnTreeView treeView, string text, int value = -1)
        {
            treeView.Nodes.Insert (0, new DnnTreeNode { Text = text, Value = value.ToString () });
        }
    }
}

