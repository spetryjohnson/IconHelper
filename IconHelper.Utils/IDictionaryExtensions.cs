using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IconHelper.Utils {
	
	public static class IDictionaryExtensions {

		/// <summary>
		/// Adds the specified item to the dictionary. If the key already exists, the value is appended
		/// to the end of the existing value using the specified delimiter.
		/// </summary>
		public static IDictionary<TKey, string> AddOrAppend<TKey>(this IDictionary<TKey, string> dict, TKey key, string value, string delimiter = ",") {
			if (dict.ContainsKey(key)) {
				dict[key] = dict[key] + delimiter + value;
			}
			else {
				dict[key] = value;
			}
			
			return dict;
		}
	}
}
