using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.Diagnostics.Contracts;

namespace IconHelper.Utils.Fakes {
	
	/// <summary>
	/// Implementation of HttpContextBase for use with tests. 
	/// </summary>
	public class FakeHttpContext : HttpContextBase {
		private HttpRequestBase _request;
		private FakeHttpSession _session;
		private Dictionary<object, object> _items;

		public FakeHttpContext() {
		}

		public FakeHttpContext(HttpRequestBase request = null, string appPath = null) {
			Contract.Requires(appPath == null || request == null, 
				"request and appPath cannot both be specified. To do this, create the request first, set the appPath, and then pass it in.");

			_request = request ?? new FakeHttpRequest(appPath: appPath);
		}

		public override HttpRequestBase Request {
			get { return _request ?? (_request = new FakeHttpRequest()); }
		}

		public override HttpResponseBase Response {
			get { return new HttpResponseWrapper(new HttpResponse(new StreamWriter(Stream.Null))); }
		}

		public override HttpSessionStateBase Session {
			get { return _session ?? (_session = new FakeHttpSession(new SessionStateItemCollection())); }
		}

		public override IDictionary Items {
			get { return _items ?? (_items = new Dictionary<object,object>()); }
		}
	}
}
