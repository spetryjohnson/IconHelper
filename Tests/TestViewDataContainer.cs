﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace IconHelper.Tests {
	
	public class TestViewDataContainer : IViewDataContainer {
		public ViewDataDictionary ViewData { get; set; }

		public TestViewDataContainer(ViewDataDictionary p_viewdata) {
			ViewData = p_viewdata;
		}
	}
}
