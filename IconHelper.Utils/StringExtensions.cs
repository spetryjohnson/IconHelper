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
				? returnIfNullOrEmpty
				: value;
		}

		/// <summary>
		/// Returns TRUE if the string is either null or empty, FALSE otherwise. Syntactic sugar 
		/// for String.IsNullOrEmpty()
		/// </summary>
		public static bool IsNullOrEmpty(this string value) {
			return String.IsNullOrEmpty(value);
		}

		/// <summary>
		/// Returns TRUE if the string is not null and contains a non-empty value. Returns FALSE if the 
		/// value is null or empty. Syntactic sugar over String.IsNullOrEmpty()
		/// </summary>
		public static bool IsNotNullOrEmpty(this string value) {
			return !String.IsNullOrEmpty(value);
		}
	}
}
