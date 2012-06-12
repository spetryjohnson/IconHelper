using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.Contracts;

namespace IconHelper {

	/// <summary>
	/// Attribute that lets us associate metadata such as image path, default alt text, etc on
	/// an Enum representing the known icons in a project.
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