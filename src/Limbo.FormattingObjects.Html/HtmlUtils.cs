using Limbo.FormattingObjects.Html.Elements;
using HtmlNode = HtmlAgilityPack.HtmlNode;

namespace Limbo.FormattingObjects.Html;

public static class HtmlUtils {

    public static HtmlAttributeList ParseAttributes(HtmlNode node) {

        HtmlAttributeList attributes = new();

        foreach (HtmlAgilityPack.HtmlAttribute attr in node.Attributes) {
            attributes[attr.Name] = attr.Value;
        }

        return attributes;

    }

}