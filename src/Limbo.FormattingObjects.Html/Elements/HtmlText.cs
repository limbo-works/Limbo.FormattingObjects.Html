namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlText : HtmlNode {

    public string Value { get; internal set; }

    public HtmlText(HtmlAgilityPack.HtmlNode node, string value) {
        Value = value;
    }

}