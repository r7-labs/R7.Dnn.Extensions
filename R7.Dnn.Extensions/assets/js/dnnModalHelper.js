﻿(function (window) {
  window.dnnModalHelper = {
    getPopup: function () {
      return window.parent.jQuery("#iPopUp");
    },
    disableRefresh: function () {
      var popup = this.getPopup();
      if (popup.length === 1) {
	    if (popup.dialog("option", "refresh")) {
	      window.parent.dnnModalHelperClose = popup.dialog("option", "close");
	      popup.dialog("option", "refresh", false);
	      popup.dialog("option", "close", null);
	    }
      }
    },
    reEnableRefresh: function () {
      var popup = this.getPopup();
      if (popup.length === 1) {
        popup.dialog("option", "refresh", true);
        popup.dialog("option", "close", window.parent.dnnModalHelperClose);
      }
    }
  }
}) (window);