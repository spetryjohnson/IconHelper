using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IconHelper.Tests {
	
	/// <summary>
	/// Base class for testing Html Helper classes. Mocks out the helper instance.
	/// </summary>
	public class HtmlHelperTestBase {

		public TestHtmlHelper Html { get; set; }

		public HtmlHelperTestBase() {
			Html = TestHtmlHelper.Create();
		}
	}
}
