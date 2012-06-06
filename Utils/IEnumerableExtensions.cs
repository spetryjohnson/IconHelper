using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IconHelper.Utils {
	
	public static class IEnumerableExtensions {

		/// <summary>
		/// Returns the first item from a list. Throws an exception with the specified message if an item could not be
		/// returned because the sequence was either null or empty.
		/// </summary>
		public static T FirstOrThrow<T>(this IEnumerable<T> sequence, string failureMsg) {
			if (sequence.IsNullOrEmpty()) {
				throw new ArgumentOutOfRangeException(failureMsg + " [Could not take first item; the sequence was null or empty]");
			}

			return sequence.First();
		}

		/// <summary>
		/// Returns TRUE if the sequence is either null or empty.
		/// </summary>
		public static bool IsNullOrEmpty(this IEnumerable sequence) {
			if (sequence == null) 
				return true;

			return sequence.GetEnumerator().MoveNext() == false;
		}
	}
}
