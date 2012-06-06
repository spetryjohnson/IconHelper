using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IconHelper.Utils;
using IconHelper.Web.Models;

namespace IconHelper.Web.Helpers {

	/// <summary>
	/// Html Helper class that exposes methods for rendering common site icons.
	/// </summary>
	public static class IconExtensions {

		/// <summary>
		/// Renders an icon image. 
		/// </summary>
		/// <param name="title">The title to use. If NULL, defaults to the value stored on the icon.</param>
		/// <param name="alt">The alt text. If NULL, defaults to the value stored on the icon.</param>
		/// <param name="url">A URL to link to. If not NULL, a link tag to this value is wrapped around the icon.</param>
		/// <param name="onclick">The onclick handler to render.</param>
		/// <param name="cssClass">The CSS class to assign to the image.</param>
		/// <param name="attributes">A dictionary of additional html attributes to render.</param>
		public static IHtmlString Icon(this HtmlHelper html, 
			Enum icon, 
			string title = null, 
			string alt = null,
			string url = null,
			string onclick = null,
			string cssClass = null,
			object attributes = null) {

			var iconData = new IconMetaData(icon);

			return html.Icon(iconData, title: title, alt: alt, url: url, onclick: onclick, cssClass: cssClass, attributes: attributes);
		}

		/// <summary>
		/// Renders an icon image. 
		/// </summary>
		/// <param name="title">The title to use. If NULL, defaults to the value stored on the icon.</param>
		/// <param name="alt">The alt text. If NULL, defaults to the value stored on the icon.</param>
		/// <param name="url">A URL to link to. If not NULL, a link tag to this value is wrapped around the icon.</param>
		/// <param name="onclick">The onclick handler to render.</param>
		/// <param name="cssClass">The CSS class to assign to the image.</param>
		/// <param name="attributes">A dictionary of additional html attributes to render.</param>
		public static IHtmlString Icon(this HtmlHelper html,
			IIconMetaData icon,
			string title = null,
			string alt = null,
			string url = null,
			string onclick = null,
			string cssClass = null,
			object attributes = null) {

			var image = new TagBuilder("img");
			//image.Attributes.Add("alt", alt);
			//image.Attributes.Add("title", title);
			//image.MergeAttributes(htmlAttributes.ToHtmlAttributeDictionary(), true);

			image.Attributes.Add("src", icon.Filename);

			return new HtmlString(
				image.ToString(TagRenderMode.SelfClosing)
			);
		}
	}
}