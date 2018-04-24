//
//  EditPortalModuleBase.cs
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

using System;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.UserControls;
using R7.Dnn.Extensions.Data;
using R7.Dnn.Extensions.Text;
using R7.Dnn.Extensions.Urls;
using DnnLocalization = DotNetNuke.Services.Localization.Localization;

namespace R7.Dnn.Extensions.Modules
{
    /// <summary>
    /// A base class to build simple edit module controls
    /// </summary>
    public abstract class EditPortalModuleBase<TItem, TKey>: PortalModuleBase
        where TItem : class, new()
        where TKey : struct
    {
        #region Fields & Properties

        /// <summary>
        /// The edited item identifier.
        /// </summary>
        protected virtual TKey? ItemKey {
            get {
                var itemKeyObj = ViewState ["ItemKey"];
                if (itemKeyObj != null) {
                    return (TKey?) itemKeyObj;
                }

                // parse querystring parameters
                var itemKey = ParseHelper.ParseToNullable<TKey> (Request.QueryString [Key]);
                ViewState ["ItemKey"] = itemKey;
                return itemKey;
            }
            set { ViewState ["ItemKey"] = value; }
        }

        /// <summary>
        /// The querystring key to parse to get edited item identifier.
        /// </summary>
        protected readonly string Key;

        #endregion

        #region Controls

        /// <summary>
        /// The update button.
        /// </summary>
        protected LinkButton ButtonUpdate;

        /// <summary>
        /// The delete button.
        /// </summary>
        protected LinkButton ButtonDelete;

        /// <summary>
        /// The cancel link.
        /// </summary>
        protected HyperLink LinkCancel;

        /// <summary>
        /// The module audit control.
        /// </summary>
        protected ModuleAuditControl ModuleAuditControl;

        protected ICrudProvider<TItem> CrudProvider;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="R7.Dnn.Extensions.Modules.EditPortalModuleBase{TItem,TKey}"/> class.
        /// </summary>
        /// <param name="key">Querystring key.</param>
        /// <param name="crudProvider">CRUD provider object.</param>
        protected EditPortalModuleBase (string key, ICrudProvider<TItem> crudProvider)
        {
            Key = key;
            CrudProvider = crudProvider;
        }

        /// <summary>
        /// Handles Page_Init event
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnInit (EventArgs e)
        {
            base.OnInit (e);

            InitControls ();

            // wireup handlers
            ButtonUpdate.Click += OnButtonUpdateClick;
            ButtonDelete.Click += OnButtonDeleteClick;

            // set url for Cancel link
            LinkCancel.NavigateUrl = UrlHelper.GetCancelUrl (UrlUtils.InPopUp ());

            // add confirmation dialog to delete button
            ButtonDelete.Attributes.Add ("onclick", $"javascript:return confirm('{DnnLocalization.GetString ("DeleteItem")}');");
        }

        /// <summary>
        /// Init controls, required by <see cref="R7.Dnn.Extensions.Modules.EditPortalModuleBase{TItem,TKey}"/>
        /// Provides interface to implement OnInitControls in child classes.
        /// </summary>
        /// <param name="buttonUpdate">Update linkbutton.</param>
        /// <param name="buttonDelete">Delete linkbutton.</param>
        /// <param name="linkCancel">Cancel hyperlink.</param>
        /// <param name="moduleAuditControl">Optional module audit control.</param>
        protected void InitControls (LinkButton buttonUpdate, LinkButton buttonDelete, HyperLink linkCancel, ModuleAuditControl moduleAuditControl = null)
        {
            ButtonUpdate = buttonUpdate;
            ButtonDelete = buttonDelete;
            LinkCancel = linkCancel;
            ModuleAuditControl = moduleAuditControl;
        }

        /// <summary>
        /// Handles the Page_Load event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                if (!IsPostBack) {
                    if (Request.QueryString [Key] != null) {
                        if (ItemKey != null) {
                            var item = GetItemWithDependencies (ItemKey.Value);
                            if (item != null) {
                                if (CanEditItem (item)) {
                                    ButtonDelete.Visible = CanDeleteItem (item);
                                    LoadItem (item);
                                }
                            }
                            else {
                                Exceptions.LogException (new SecurityException ($"Wrong item key: {ItemKey}"));
                            }
                        }
                        else {
                            if (CanAddItem ()) {
                                ButtonDelete.Visible = false;
                                if (ModuleAuditControl != null) {
                                    ModuleAuditControl.Visible = false;
                                }

                                LoadNewItem ();
                            }
                        }
                    }
                    else {
                        Exceptions.LogException (new SecurityException ($"Wrong edit URL: {Request.RawUrl}"));
                    }
                }
                else {
                    PostBack ();
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        /// <summary>
        /// Handles item update button click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnButtonUpdateClick (object sender, EventArgs e)
        {
            try {
                if (Page.IsValid) {
                    var item = GetItem (ItemKey.Value);
                    var isNew = item == null;

                    if (isNew) {
                        item = new TItem ();
                    }

                    BeforeUpdateItem (item, isNew);

                    if (isNew) {
                        AddItem (item);
                    }
                    else {
                        UpdateItem (item);
                    }

                    AfterUpdateItem (item, isNew);

                    // synchronize module
                    ModuleController.SynchronizeModule (ModuleId);

                    Response.Redirect (Globals.NavigateURL (), true);
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        /// <summary>
        /// Handles item delete button click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnButtonDeleteClick (object sender, EventArgs e)
        {
            try {
                var item = GetItem (ItemKey.Value);
                if (item != null && CanDeleteItem (item)) {
                    DeleteItem (item);
                    Response.Redirect (Globals.NavigateURL (), true);
                }
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #region CRUD methods

        // TODO: Remove as too children-specific?
        /// <summary>
        /// Override this method if you need extra data 
        /// (e.g. some dependent objects and collections) 
        /// to fill edit form in LoadItem method, 
        /// than returned by GetItem method.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="itemId">Item identifier.</param>
        protected virtual TItem GetItemWithDependencies (TKey itemId)
        {
            return GetItem (itemId);
        }

        /// <summary>
        /// Gets the key of the item.
        /// </summary>
        /// <returns>The key of the item.</returns>
        /// <param name="item">Item.</param>
        protected abstract TKey GetItemKey (TItem item);

        /// <summary>
        /// Implement method which will get item by id.
        /// Usually there is no need to return extra data
        /// (e.g. some dependent objects or collections) here.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="itemKey">Item key.</param>
        protected virtual TItem GetItem (TKey itemKey) => CrudProvider.Get (itemKey);

        /// <summary>
        /// Implement method which will store new item in the datastore
        /// </summary>
        /// <param name="item">Item.</param>
        protected virtual void AddItem (TItem item) => CrudProvider.Add (item);

        /// <summary>
        /// Implement method which will update existing item in the datastore
        /// </summary>
        /// <param name="item">Item.</param>
        protected virtual void UpdateItem (TItem item) => CrudProvider.Update (item);

        /// <summary>
        /// Implement method which deletes the item in the datastore
        /// </summary>
        /// <param name="item">Item.</param>
        protected virtual void DeleteItem (TItem item) => CrudProvider.Delete (item);

        #endregion

        #region Other extension points

        /// <summary>
        /// Implement to pass references to the required module controls
        /// </summary>
        /// <example>
        /// <code>
        /// protected override void OnInitControls () 
        /// {
        ///    InitControls (buttonUpdate, buttonDelete, linkCancel);
        /// }
        /// </code>
        /// </example>
        protected abstract void InitControls ();

        /// <summary>
        /// Implement to provide item to form controls binding code here.
        /// </summary>
        /// <param name="item">Item.</param>
        protected abstract void LoadItem (TItem item);

        /// <summary>
        /// Override to provide custom code which should be called for new items.
        /// </summary>
        protected virtual void LoadNewItem ()
        { }

        // TODO: Rename to LoadPostBack?
        /// <summary>
        /// Override to provide code which should be called on Page_Load then (IsPostBack == true) here
        /// </summary>
        protected virtual void PostBack ()
        { }

        /// <summary>
        /// Implement to provide code to fill item from form controls here.
        /// </summary>
        /// <param name="item">Item.</param>
        protected abstract void BeforeUpdateItem (TItem item, bool isNew);

        /// <summary>
        /// Implement to provide code which will be called 
        /// after item update in the DB
        /// </summary>
        /// <param name="item">Item.</param>
        protected virtual void AfterUpdateItem (TItem item, bool isNew)
        { }

        // TODO: Extract CRUD security provider

        /// <summary>
        /// Override to define edit permission checks here.
        /// </summary>
        /// <returns><c>true</c> if the specified item can be edited; otherwise, <c>false</c>.</returns>
        /// <param name="item">Item.</param>
        protected virtual bool CanEditItem (TItem item)
        {
            return true;
        }

        protected virtual bool CanAddItem ()
        {
            return true;
        }

        /// <summary>
        /// Override to define delete permission checks here.
        /// </summary>
        /// <returns><c>true</c> if the specified item can be deleted; otherwise, <c>false</c>.</returns>
        /// <param name="item">Item.</param>
        protected virtual bool CanDeleteItem (TItem item)
        {
            return true;
        }

        /// <summary>
        /// Override to define custom action on control load then item does not exists.
        /// </summary>
        protected virtual void ItemDoesNotExists ()
        {
            Response.Redirect (Globals.NavigateURL (), true);
        }

        #endregion
    }
}
