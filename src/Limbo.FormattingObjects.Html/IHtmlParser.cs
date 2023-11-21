using Limbo.FormattingObjects.Html.Elements;

namespace Limbo.FormattingObjects.Html {

    public interface IHtmlParser {

        HtmlElement Parse(string html);

    }

}