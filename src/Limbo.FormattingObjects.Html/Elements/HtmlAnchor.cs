namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlAnchor : HtmlElement {

    public string Href {
        get { return AttributeValue("href"); }
        set { SetAttributeValue("href", value);}
    }

    public HtmlAnchor() : base("a") { }

}