#pragma warning disable CS8632

namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlImg : HtmlElement {

    public string? Src {
        get { return AttributeValue("src"); }
        set { SetAttributeValue("src", value); }
    }

    public int Width {
        get => int.TryParse(AttributeValue("width"), out int value) ? value : 0;
    }

    public int Height {
        get => int.TryParse(AttributeValue("height"), out int value) ? value : 0;
    }

    public HtmlImg(HtmlAgilityPack.HtmlNode node) : base(node) { }

}