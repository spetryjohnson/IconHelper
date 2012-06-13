using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IconHelper.Utils {
	public static class ObjectExtensions {

		/// <summary>
		/// Creates a dictionary from the specified object. Each public property becomes a key mapped to the
		/// property value. 
		/// 
		/// This is specifically designed for use with HTML helpers and anonymous objects representing HTML
		/// element attributes. It converts underscores in property names into hyphens (because C# doesn't
		/// allow hyphens in property names and there would otherwise be no way to represent HTML 5 "data" 
		/// attributes in anonymous objects).
		/// </summary>
		public static Dictionary<string, object> ToHtmlAttributeDictionary(this object obj) {
			var dictionary = new Dictionary<string, object>();

			if (obj == null) {
				return dictionary;
			}

			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj)) {
				var nameWithHyphens = property.Name.Replace("_", "-");
				object value = property.GetValue(obj);

				dictionary.Add(nameWithHyphens, value);
			}

			return dictionary;
		}
	}
}
