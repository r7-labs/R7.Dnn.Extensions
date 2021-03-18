using System;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.UserControls;
using R7.Dnn.Extensions.Models;
using R7.Dnn.Extensions.Text;
using R7.Dnn.Extensions.Urls;
using DnnLocalization = DotNetNuke.Services.Localization.Localization;

namespace R7.Dnn.Extensions.Modules
{
    /// <summary>
    /// A base class to build simple edit module controls
    /// </summary>
    public abstract class EditPortalModuleBase2<TItem, TKey>: PortalModuleBase
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
        /// Gets the mode from querystring.
        /// </summary>
        /// <value>The mode.</value>
        protected EditPortalModuleMode Mode {
            get {
                if (!Enum.TryParse (Request.QueryString ["mode"], true, out EditPortalModuleMode mode)) {
                    mode = EditPortalModuleMode.Default;
                }
                return mode;
            }
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

        /// <summary>
        /// The CRUD operations provider.
        /// </summary>
        protected ICrudProvider<TItem> CrudProvider;

        #endregion

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="key">Querystring key.</param>
        protected EditPortalModuleBase2 (string key)
        {
            Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="key">Querystring key.</param>
        /// <param name="crudProvider">CRUD provider object.</param>
        protected EditPortalModuleBase2 (string key, ICrudProvider<TItem> crudProvider)
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

            OnMapControls ();

            // wireup handlers
            ButtonUpdate.Click += OnButtonUpdateClick;
            ButtonDelete.Click += OnButtonDeleteClick;

            // set url for Cancel link
            LinkCancel.NavigateUrl = UrlHelper.GetCancelUrl (UrlUtils.InPopUp ());

            // add confirmation dialog to delete button
            ButtonDelete.Attributes.Add ("onclick", $"javascript:return confirm('{DnnLocalization.GetString ("DeleteItem")}');");
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
                    if (ItemKey == null || Mode == EditPortalModuleMode.Add) {
                        if (CanAddItem ()) {
                            ButtonDelete.Visible = false;
                            if (ModuleAuditControl != null) {
                                ModuleAuditControl.Visible = false;
                            }

                            LoadNewItem ();
                        }
                    }
                    else if (ItemKey != null) {
                        var item = GetItem (ItemKey.Value);
                        if (item != null) {
                            if (CanEditItem (item)) {
                                ButtonDelete.Visible = CanDeleteItem (item);
                                LoadItem (item);
                            }
                        }
                        else {
                            throw new Exception ($"Wrong item key: {ItemKey}");
                        }
                    }
                    else {
                        throw new Exception ($"Wrong edit URL: {Request.RawUrl}");
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
                    var item = SafeGetItem (ItemKey);
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
                var item = SafeGetItem (ItemKey);
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

        TItem SafeGetItem (TKey? itemKey)
        {
            return (itemKey != null) ? GetItem (itemKey.Value) : null;
        }

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
        /// Implement to pass references to the required module controls.
        /// </summary>
        protected abstract void OnMapControls ();

        /// <summary>
        /// Provides default implementation for OnMapControls body in child classes.
        /// </summary>
        /// <param name="controls">Controls to map</param>
        protected virtual void MapControls (EditPortalModuleBase2Controls controls)
        {
            ButtonUpdate = controls.btnUpdate;
            ButtonDelete = controls.btnDelete;
            LinkCancel = controls.lnkCancel;
            ModuleAuditControl = controls.ctlAudit;
        }
        
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
        /// <param name="isNew">Adding new item?</param>
        protected abstract void BeforeUpdateItem (TItem item, bool isNew);

        /// <summary>
        /// Implement to provide code which will be called 
        /// after item update in the DB
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="isNew">Adding new item?</param>
        protected virtual void AfterUpdateItem (TItem item, bool isNew)
        { }

        // TODO: Extract CRUD security provider?

        /// <summary>
        /// Override to define edit item permission checks here.
        /// </summary>
        /// <returns><c>true</c> if the specified item can be edited; otherwise, <c>false</c>.</returns>
        /// <param name="item">Item.</param>
        protected virtual bool CanEditItem (TItem item)
        {
            return true;
        }

        /// <summary>
        /// Override to define add item permission checks here.
        /// </summary>
        /// <returns><c>true</c> if the item can be added; otherwise, <c>false</c>.</returns>
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

        #endregion
    }
}
