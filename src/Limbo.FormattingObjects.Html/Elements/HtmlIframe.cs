#pragma warning disable CS8632

namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlIframe : HtmlElement {

    public string? Src {
        get { return AttributeValue("src"); }
        set { SetAttributeValue("src", value); }
    }

    public HtmlIframe() : base("iframe") { }

    public HtmlIframe(HtmlAgilityPack.HtmlNode node) : base(node) { }

}