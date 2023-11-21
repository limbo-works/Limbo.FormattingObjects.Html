using Newtonsoft.Json;

namespace Limbo.FormattingObjects.Html.Elements;

public abstract class HtmlNode {

    [JsonIgnore]
    public HtmlElement Parent { get; internal set; }

    [JsonIgnore]
    public HtmlNode Previous { get; internal set; }

    [JsonIgnore]
    public HtmlNode Next { get; internal set; }

    [JsonIgnore]
    public HtmlElement PreviousElement {
        get {
            HtmlNode prev = Previous;
            while (prev is HtmlText) {
                prev = prev.Previous;
            }
            return prev as HtmlElement;
        }
    }

}