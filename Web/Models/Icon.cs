using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IconHelper.Web.Models {

	/// <summary>
	/// Enumerates the standard icons used in this system.
	/// </summary>
	public enum Icon {

		[Icon("~/Content/icon-delete.png", title: "Click here to delete this item", alt: "Delete")]
		Delete,

		[Icon("~/Content/icon-checkmark.png", title: "This is a checkmark icon!", alt: "Checkmark")]
		Checkmark
	}
}