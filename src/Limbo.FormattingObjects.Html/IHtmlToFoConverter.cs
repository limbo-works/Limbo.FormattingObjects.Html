using Limbo.FormattingObjects.Html.Elements;

#pragma warning disable CS8632

namespace Limbo.FormattingObjects.Html;

public interface IHtmlToFoConverter {

    FoElement? Convert(HtmlNode html);

}