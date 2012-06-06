using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IconHelper.Utils;
using IconHelper.Web.Models;

namespace IconHelper.Web.Helpers {

	/// <summary>
	/// Represents a set of metadata about an icon. This interface gives us a consistent (and test-friendly) 
	/// way of representing this data when passing it to the rendering logic. Specifically, this allows
	/// for icon data to be represented by decorating enums with attributes as well as normal properties
	/// of an object instance. 
	/// </summary>
	public interface IIconMetaData { 
		string Filename { get; }
		string AltText { get; }
		string Title { get; }
	}

	/// <summary>
	/// Adapter class that converts an Icon enum into an IIconMetaData instance.
	/// 
	/// This is used internally by the icon helper extensions.
	/// </summary>
	public class IconMetaData : IIconMetaData {
		public string Filename { get; set; }
		public string AltText { get; set; }
		public string Title { get; set; }

		public IconMetaData(Enum iconEnum) {
			var iconAttr = iconEnum.GetAttributes<IconAttribute>()
				.FirstOrThrow("Could not find any icon attribute data for " + iconEnum.ToString());

			this.Filename = iconAttr.FileName;
			this.AltText = iconAttr.AltText;
			this.Title = iconAttr.Title;
		}
	}
}