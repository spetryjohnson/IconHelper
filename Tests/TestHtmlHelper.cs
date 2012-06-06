using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Rhino.Mocks;
using System.Web.Routing;

namespace IconHelper.Tests {
	
	/// <summary>
	/// Test-specific subclass for HtmlHelper. Exposes the ViewData collection for 
	/// easy access.
	/// </summary>
	public class TestHtmlHelper : HtmlHelper {
		public new IViewDataContainer ViewData { get; set; }

		public TestHtmlHelper(ViewContext p_viewContext, IViewDataContainer p_viewdata)
			: base(p_viewContext, new TestViewDataContainer(new ViewDataDictionary())) {
			this.ViewData = p_viewdata;
		}

		public static TestHtmlHelper Create(string appPath = "/") {
			var mockery = new MockRepository();
			var httpContext = new MvcContrib.TestHelper.Fakes.FakeHttpContext(appPath);
			var controllerContext = new ControllerContext(httpContext, new RouteData(), mockery.DynamicMock<ControllerBase>());
			var view = mockery.DynamicMock<IView>();
			var viewData = new ViewDataDictionary(new { });
			var viewContext = new ViewContext(controllerContext, view, viewData, new TempDataDictionary(), new System.IO.StringWriter());
			var helper = new TestHtmlHelper(viewContext, new TestViewDataContainer(viewData));

			return helper;
		}
	}
}
