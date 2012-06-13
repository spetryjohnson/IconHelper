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
		public void Can_specify_title_that_overrides_the_metadata() {
			var html = Html.Icon(TestIcon.SampleIcon, title: "Override").ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("title=\"Override\""));
			Assert.That(html, Is.Not.StringContaining("Sample title"));
		}

		[Test]
		public void Can_specify_alt_text_that_overrides_the_metadata() {
			var html = Html.Icon(TestIcon.SampleIcon, alt: "Override").ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("alt=\"Override\""));
			Assert.That(html, Is.Not.StringContaining("Sample alt"));
		}

		[Test]
		public void Can_specify_a_CSS_class_for_the_image_tag() {
			var html = Html.Icon(TestIcon.SampleIcon, cssClass: "my-class").ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringContaining("class=\"my-class\""));
		}

		[Test]
		public void Can_specify_an_onclick_handler_for_the_image_tag() {
			var html = Html.Icon(TestIcon.SampleIcon, onclick: "alert('Foo'); return false;").ToString();

			Console.WriteLine(html);

			var expected = String.Format("onclick=\"{0}\"", Html.AttributeEncode("alert('Foo'); return false;"));

			Assert.That(html, Is.StringContaining(expected));
		}

		[Test]
		public void Can_specify_additional_html_attributes_in_a_dictionary() {
			var html = Html.Icon(TestIcon.SampleIcon, cssClass: "my-class", attributes: new { @class = "second-class", tabindex = "5" }).ToString();

			Console.WriteLine(html);

			var expected = String.Format("onclick=\"{0}\"", Html.AttributeEncode("alert('Foo'); return false;"));

			Assert.That(html, Is.StringMatching("<img.* class=\"my-class second-class"), "Did not merge CSS classes");
			Assert.That(html, Is.StringMatching("<img.* tabindex=\"5\""), "Did not add new attribute");
		}

		[Test]
		public void Underscores_in_custom_attribute_names_are_converted_to_hyphens() {
			var html = Html.Icon(TestIcon.SampleIcon, attributes: new { data_attr = "test" }).ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringMatching("<img.* data-attr=\"test\""));
		}

		[Test]
		public void Specifying_an_onclick_handler_turns_the_cursor_into_a_pointer() {
			var html = Html.Icon(TestIcon.SampleIcon, onclick: "alert('Foo');").ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringMatching("<img.*cursor: pointer"));
		}

		[Test]
		public void Specifying_an_onclick_handler_adds_a_CSS_class_to_the_image_so_that_the_pointer_style_can_be_changed_via_CSS() {
			var html = Html.Icon(TestIcon.SampleIcon, onclick: "alert('Foo');", cssClass: "my-class").ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringMatching("<img.* class=\"clickable-icon my-class"));
		}

		[Test]
		public void Specifying_a_url_causes_the_image_to_be_wrapped_in_a_link_tag_to_that_url() {
			var html = Html.Icon(TestIcon.SampleIcon, url: "/some/location.html").ToString();

			Console.WriteLine(html);

			Assert.That(html, Is.StringMatching("<a.* href=\"/some/location.html\".*>.*<img.*</a>"));
		}

		[Test]
		public void Escapes_double_quotes_in_title() {
			var html = Html.Icon(TestIcon.WithQuotesInMeta).ToString();

			Console.WriteLine(html);

			var expected = String.Format("title=\"{0}\"", Html.AttributeEncode("Title with \"quotes and 'such"));

			Assert.That(html, Is.StringContaining(expected));
		}

		[Test]
		public void Escapes_double_quotes_in_alt_text() {
			var html = Html.Icon(TestIcon.WithQuotesInMeta).ToString();

			Console.WriteLine(html);

			var expected = String.Format("alt=\"{0}\"", Html.AttributeEncode("Alt with \"quotes and 'such"));

			Assert.That(html, Is.StringContaining(expected));
		}

		[Test]
		public void Escapes_double_quotes_in_onclick_handler() {
			var html = Html.Icon(TestIcon.SampleIcon, onclick: "alert(\"foo\");").ToString();

			Console.WriteLine(html);

			var expected = String.Format("onclick=\"{0}\"", Html.AttributeEncode("alert(\"foo\");"));

			Assert.That(html, Is.StringContaining(expected));
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
