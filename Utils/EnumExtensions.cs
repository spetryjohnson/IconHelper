using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IconHelper.Utils {

	public static class EnumExtensions {

		/// <summary>
		/// Given an attribute type T, returns an array of all attributes of that type that are
		/// associated with the specified enum value.
		/// </summary>
		public static T[] GetAttributes<T>(this Enum p_enumValue) where T : Attribute {
			try {
				string enumString = p_enumValue.ToString();

				var attributes = (T[])p_enumValue
					.GetType()
					.GetField(enumString)
					.GetCustomAttributes(typeof(T), false);

				return attributes;
			}
			catch (Exception ex) {
				throw new ApplicationException("Cannot get attributes for enum type " + p_enumValue.GetType().ToString() + ", value " + p_enumValue.ToString() + ": " + ex.Message, ex);
			}
		}
	}
}
