using Limbo.FormattingObjects;
using Limbo.FormattingObjects.Html;
using Limbo.FormattingObjects.Html.Elements;

namespace TestProject1;

[TestClass]
public class UnitTest1 {

    [TestMethod]
    public void Paragraphs() {

        const string html = """
            <p><strong>Hello World</strong></p>
            <p>Hej verden</p>
            <p><em>Hallo Welt</em></p>
            <p><span>Hej världen</span></p>
            """;

        const string expected = """
            <block-container xmlns="http://www.w3.org/1999/XSL/Format">
              <block margin-top="10px">
                <inline font-weight="bold"><![CDATA[Hello World]]></inline>
              </block>
              <block margin-top="10px"><![CDATA[Hej verden]]></block>
              <block margin-top="10px">
                <inline font-style="italic"><![CDATA[Hallo Welt]]></inline>
              </block>
              <block margin-top="10px">
                <inline><![CDATA[Hej världen]]></inline>
              </block>
            </block-container>
            """;

        IHtmlParser parser = new HtmlParser();
        IHtmlToFoConverter converter = new HtmlToFoConverter();

        HtmlElement result = parser.Parse(html);

        FoElement? fo = converter.Convert(result);
        Assert.IsNotNull(fo);

        string actual = fo.ToString();

        Assert.AreEqual(expected, actual);

    }

    [TestMethod]
    public void UnorderedList() {

        const string html = """
            <ul>
                <li>Hello World</li>
            </ul>
            """;

        const string expected = """
            <block-container xmlns="http://www.w3.org/1999/XSL/Format">
              <list-block margin-top="15px">
                <list-item margin-top="5px">
                  <list-item-label start-indent="15px"><![CDATA[•]]></list-item-label>
                  <list-item-body start-indent="35px"><![CDATA[Hello World]]></list-item-body>
                </list-item>
              </list-block>
            </block-container>
            """;

        IHtmlParser parser = new HtmlParser();
        IHtmlToFoConverter converter = new HtmlToFoConverter();

        HtmlElement result = parser.Parse(html);

        FoElement? fo = converter.Convert(result);
        Assert.IsNotNull(fo);

        string actual = fo.ToString();

        Assert.AreEqual(expected, actual);

    }

}