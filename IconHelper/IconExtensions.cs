using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using IconHelper.Utils;

namespace IconHelper {

	/// <summary>
	/// Html Helper class that exposes methods for rendering common site icons.
	/// </summary>
	public static class IconExtensions {

		/// <summary>
		/// Renders an icon image for an enum that has an associated IconAttribute.
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
		/// Renders an icon image using the specified metadata.
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
			image.Attributes.Add("alt", alt ?? icon.AltText);
			image.Attributes.Add("title", title ?? icon.Title);

			SetOnclickHandler(onclick, image);
			SetCssClass(cssClass, image);
			SetImageSrc(html, icon, image);
			MergeHtmlAttributes(attributes, image);

			var imageTagHtml = new HtmlString(
				image.ToString(TagRenderMode.SelfClosing)
			);

			if (url.IsNotNullOrEmpty()) {
				imageTagHtml = new HtmlString(
					String.Format("<a href=\"{0}\">{1}</a>", url, imageTagHtml.ToString())
				);
			}

			return imageTagHtml;
		}

		private static void SetOnclickHandler(string onclick, TagBuilder image) {
			if (onclick.IsNotNullOrEmpty()) {
				image.Attributes.AddOrAppend("onclick", onclick, ";");
				image.Attributes.AddOrAppend("style", "cursor: pointer", ";");
				image.Attributes.AddOrAppend("class", "clickable-icon", " ");
			}
		}

		private static void SetCssClass(string cssClass, TagBuilder image) {
			if (cssClass.IsNotNullOrEmpty()) {
				image.Attributes.AddOrAppend("class", cssClass, " ");
			}
		}

		private static void SetImageSrc(HtmlHelper html, IIconMetaData icon, TagBuilder image) {
			var imagePath = icon.Filename;

			// filenames starting with a "~" are treated as relative to app root
			if (imagePath.StartsWith("~/")) {
				var pathWithoutTilde = imagePath.Substring(2);
				var appRoot = html.ViewContext.HttpContext.Request.ApplicationPath;

				imagePath = Path.Combine(appRoot, pathWithoutTilde);
			}

			image.Attributes.Add("src", imagePath);
		}

		/// <summary>
		/// Converts the attribute object into a name/value object representing HTML attributes
		/// and then merges them into the image's attributes object. 
		/// 
		/// Values for keys such as "class", "style" and "onclick" are appended to existing
		/// values for those keys.
		/// 
		/// Values for other keys (where we don't know which delimiter to use for joining)
		/// just replace any existing value.
		/// </summary>
		private static void MergeHtmlAttributes(object attributes, TagBuilder image) {
			var customAttrs = attributes.ToHtmlAttributeDictionary();

			foreach (var keyValuePair in customAttrs) {
				if (keyValuePair.Value == null)
					continue;

				var key = keyValuePair.Key.ToLower();
				var value = keyValuePair.Value.ToString();

				switch (key) {
					case "class":
						image.Attributes.AddOrAppend(key, value, " ");
						break;

					case "onclick":
					case "style":
						image.Attributes.AddOrAppend(key, value, "; ");
						break;

					default:
						image.Attributes[key] = value;
						break;
				}
			}
		}
	}
}