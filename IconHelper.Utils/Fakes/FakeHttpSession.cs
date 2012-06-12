using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Collections;
using System.Collections.Specialized;

namespace IconHelper.Utils.Fakes {
	
	/// <summary>
	/// Implements HttpSessionStateBase using a dictionary for use in tests.
	/// </summary>
	public class FakeHttpSession : HttpSessionStateBase {
		private readonly SessionStateItemCollection _items;

		public FakeHttpSession(SessionStateItemCollection items) {
			_items = items;
		}

		public override void Abandon() {
			_items.Clear();
		}

		public override void Add(string key, object value) {
			_items[key] = value;
		}

		public override void Clear() {
			_items.Clear();
		}

		public override int Count {
			get { return _items.Count; }
		}

		public override IEnumerator GetEnumerator() {
			return _items.GetEnumerator();
		}

		public override NameObjectCollectionBase.KeysCollection Keys {
			get { return _items.Keys; }
		}

		public override void Remove(string key) {
			_items.Remove(key);
		}

		public override object this[int index] {
			get { return _items[index]; }
			set { _items[index] = value; }
		}

		public override object this[string key] {
			get { return _items[key]; }
			set { _items[key] = value; }
		}
	}
}
