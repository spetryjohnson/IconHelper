using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IconHelper.Utils;
using IconHelper.Web.Helpers;
using IconHelper.Web.Models;

namespace IconHelper.Tests.Helpers {
	
	[TestFixture]
	public class IconExtensionsTests : HtmlHelperTestBase {

		public enum TestIcon {
			[Icon("sample.png", title: "Sample title", alt: "Sample alt")]
			SampleIcon,

			[Icon("~/sample.png", title: "With Tilde", alt: "Tilde")]
			StartsWithTilde,
		}

		[Test]
		public void Generates_image_icon_and_sets_src_equal_to_filename_associated_with_the_enum() {
			var icon = TestIcon.SampleIcon;
			
			var expectedSrc = icon.GetAttributes<IconAttribute>()
				.FirstOrThrow("No icon attributes found on test icon!")
				.FileName;

			var html = Html.Icon(icon).ToString();

			Assert.That(html, Is.StringContaining(expectedSrc));
		}

		[Test]
		public void Paths_starting_with_tilde_are_expanded_using_app_root() {
			var icon = TestIcon.StartsWithTilde;

			var expectedSrc = icon.GetAttributes<IconAttribute>()
				.FirstOrThrow("No icon attributes found on test icon!")
				.FileName;

			var html = Html.Icon(icon).ToString();

			Assert.That(html, Is.StringContaining("TODO"));
		}
	}
}
