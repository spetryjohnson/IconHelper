using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using IconHelper.Utils;

namespace IconHelper.Tests {
	
	[TestFixture]
	public class IconExtensionsTests : HtmlHelperTestBase {

		public enum TestIcon {
			[Icon("sample1.png", title: "Sample title", alt: "Sample alt")]
			SampleIcon,

			[Icon("~/icons/withTilde.png", title: "With Tilde", alt: "Tilde")]
			StartsWithTilde,

			[Icon("~/icons/withQuotes.png", title: "Title with \"quotes and 'such", alt: "Alt with \"quotes and 'such")]
			WithQuotesInMeta,
		}

		[Test]
		public void Generates_image_tag() {
			var html = Html.Icon(TestIcon.SampleIcon).ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("<img"));
		}

		[Test]
		public void Image_src_equals_filename_from_metadata() {
			var html = Html.Icon(TestIcon.SampleIcon).ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("src=\"sample1.png\""));
		}

		[Test]
		public void Image_title_equals_title_from_metadata() {
			var html = Html.Icon(TestIcon.SampleIcon).ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("title=\"Sample title\""));
		}

		[Test]
		public void Image_alt_text_equals_alt_text_from_metadata() {
			var html = Html.Icon(TestIcon.SampleIcon).ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("alt=\"Sample alt\""));
		}

		[Test]
		public void Escapes_quotes_in_title() {
			var html = Html.Icon(TestIcon.WithQuotesInMeta).ToString();

			Console.WriteLine(html);

			var expected = String.Format("title=\"{0}\"", Html.AttributeEncode("Title with \"quotes and 'such"));

			Assert.That(html, Is.StringContaining(expected));
		}

		[Test]
		public void Escapes_quotes_in_alt_text() {
			var html = Html.Icon(TestIcon.WithQuotesInMeta).ToString();

			Console.WriteLine(html);

			var expected = String.Format("alt=\"{0}\"", Html.AttributeEncode("Alt with \"quotes and 'such"));

			Assert.That(html, Is.StringContaining(expected));
		}

		[Test]
		public void Can_override_title_in_metadata() {
			var html = Html.Icon(TestIcon.SampleIcon, title: "Override").ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("title=\"Override\""));
			Assert.That(html, Is.Not.StringContaining("Sample title"));
		}

		[Test]
		public void Can_override_alt_in_metadata() {
			var html = Html.Icon(TestIcon.SampleIcon, alt: "Override").ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("alt=\"Override\""));
			Assert.That(html, Is.Not.StringContaining("Sample alt"));
		}

		[Test]
		public void Paths_starting_with_tilde_are_expanded_using_app_root() {
			Html = TestHtmlHelper.Create("/myAppRoot/");

			var html = Html.Icon(TestIcon.StartsWithTilde).ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("/myAppRoot/icons/withTilde.png"));
		}
	}
}
