using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.Contracts;

namespace IconHelper.Web.Models {

	/// <summary>
	/// Metadata attribute that lets us track data such as image path, default alt text, etc for 
	/// each Icons enum value.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class IconAttribute : Attribute {
		public string FileName { get; set; }
		public string AltText { get; set; }
		public string Title { get; set; }

		public IconAttribute(string filename, string title) : this(filename, title, null) { 
		}

		public IconAttribute(string filename, string title, string alt) {
			Contract.Requires(String.IsNullOrEmpty(filename) == false, "Filename cannot be null or empty");

			this.FileName = filename;
			this.AltText = alt;
			this.Title = title;
		}
	}
}