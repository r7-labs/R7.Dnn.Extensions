(function ($, window) {
  window.dnnModalHelper = {
    getPopup: function (_window) {
      return _window.jQuery("#iPopUp");
    },
    disableRefresh: function (popup) {
      if (popup.dialog("option", "refresh")) {
        popup.dialog("option", "refresh", false);
        window.dnnModalHelper.closingUrl = popup.dialog("option", "closingUrl");
        popup.dialog("option", "close", null);
      }
    },
    enableRefresh: function (popup, closingUrl) {
      popup.dialog("option", "refresh", true);
      popup.dialog("option", "close", function() {
        window.dnnModal.closePopUp(true, window.dnnModalHelper.closingUrl);
      });
    }
  }
}) (jQuery, window);