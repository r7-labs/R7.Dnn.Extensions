(function ($, window) {
  // window - modal popup window
  // _window - parent window
  window.dnnModalHelper = {
    getPopup: function (_window) {
      return _window.jQuery("#iPopUp");
    },
    disableRefreshOnce: function (_window) {
        if (typeof _window.dnnModalHelperClose !== "undefined") {
            this.disableRefresh (_window);
        }
    },
    disableRefresh: function (_window) {
      var popup = this.getPopup (_window);
      if (popup.dialog("option", "refresh")) {
        _window.dnnModalHelperClose = popup.dialog("option", "close");
        popup.dialog("option", "refresh", false);
        popup.dialog("option", "close", null);
      }
    },
    reEnableRefresh: function (_window) {
      var popup = this.getPopup (_window);
      popup.dialog("option", "refresh", true);
      popup.dialog("option", "close", _window.dnnModalHelperClose);
    }
  }
}) (jQuery, window);