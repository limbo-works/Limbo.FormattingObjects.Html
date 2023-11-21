using skybrudDk.Pdf.Html.Styles;

namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlSpan : HtmlElement {

    public HtmlFontWeight FontWeight { get; set; }

    public HtmlFontStyle FontStyle { get; set; }

    public HtmlTextDecoration TextDecoration { get; set; }

    public HtmlSpan(HtmlAgilityPack.HtmlNode node, HtmlElement parent = null) : base(node, parent) { }

}