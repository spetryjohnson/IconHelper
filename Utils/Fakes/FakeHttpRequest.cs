using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.IO;

namespace IconHelper.Utils.Fakes {
	
	/// <summary>
	/// Implementation of HttpRequestBase for use in tests. Exposes the main collections (Form, Querystring, etc)
	/// as name/value collections that can be read and set directly in test code.
	/// </summary>
	public class FakeHttpRequest : HttpRequestBase {
		private readonly HttpCookieCollection _cookies;
		private readonly NameValueCollection _form;
		private readonly NameValueCollection _headers;
		private readonly NameValueCollection _querystring;
		private readonly NameValueCollection _request;
		private readonly NameValueCollection _serverVariables;

		private string _appPath;
		private string _httpMethod;
		private string _path;
		private string _physicalpath;
		private string _rawUrl;
		private Uri _referrer;
		private Uri _url;

		public FakeHttpRequest(
			NameValueCollection querystring = null, 
			NameValueCollection form = null, 
			NameValueCollection request = null,
			HttpCookieCollection cookies = null, 
			NameValueCollection headers = null, 
			NameValueCollection server = null,
			string appPath = null) {

			_querystring = querystring ?? new NameValueCollection();
			_form = form ?? new NameValueCollection();
			_request = request ?? new NameValueCollection();
			_cookies = cookies ?? new HttpCookieCollection();
			_headers = headers ?? new NameValueCollection();
			_serverVariables = server ?? new NameValueCollection();

			_appPath = appPath.IfNullOrEmpty("/");
		}

		public override string ApplicationPath { get { return _appPath; } }
		public override HttpCookieCollection Cookies { get { return _cookies; } }
		public override NameValueCollection Form { get { return _form; } }
		public override NameValueCollection Headers { get { return _headers; } }
		public override string HttpMethod { get { return _httpMethod; } }
		public override string Path { get { return _path; } }
		public override string PhysicalPath { get { return _physicalpath; } }
		public override NameValueCollection QueryString { get { return _querystring; } }
		public override string RawUrl { get { return _rawUrl; } }
		public override NameValueCollection ServerVariables { get { return _serverVariables; } }
		
		public override Uri Url {
			get { return _url ?? (_url = new Uri("http://www.somesite.com")); }
		}

		public override Uri UrlReferrer {
			get {
				if (_referrer == null) {
					throw new NullReferenceException("The referrer is null.");
				}

				return _referrer;
			}
		}
		
		public override string MapPath(string virtualPath) {
			return Directory.GetParent(
				Directory.GetCurrentDirectory()
			).ToString();
		}

		public void SetApplicationPath(string path) {
			_appPath = path;
		}

		public FakeHttpRequest SetHeaderValue(string key, string value) {
			_headers[key] = value;

			return this;
		}

		public void SetHttpMethod(string method) {
			_httpMethod = method;
		}

		public void SetPath(string path) {
			_path = path;
		}

		public void SetRawUrl(string url) {
			_rawUrl = url;
		}

		public FakeHttpRequest SetRequestValue(string key, string value) {
			_request[key] = value;

			return this;
		}

		public FakeHttpRequest SetReferrer(string url) {
			_referrer = new Uri(url);

			return this;
		}

		public void SetUrl(string url) {
			_url = new Uri(url);
		}

		public override string this[string key] {
			get { return _request[key]; }
		}
	}
}
