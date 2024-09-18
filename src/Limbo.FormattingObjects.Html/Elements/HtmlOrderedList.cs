namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlOrderedList : HtmlElement {

    public HtmlOrderedList() : base("ol") { }

    public HtmlOrderedList(HtmlAgilityPack.HtmlNode node) : base(node) { }

}