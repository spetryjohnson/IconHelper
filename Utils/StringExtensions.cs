using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IconHelper.Utils {

	public static class StringExtensions {

		/// <summary>
		/// Returns the string this is invoked on, unless it is null or empty (in which case the
		/// specified alternate string is returned).
		/// </summary>
		public static string IfNullOrEmpty(this string value, string returnIfNullOrEmpty) {
			return String.IsNullOrEmpty(value)
				? value
				: returnIfNullOrEmpty;
		}
	}
}
