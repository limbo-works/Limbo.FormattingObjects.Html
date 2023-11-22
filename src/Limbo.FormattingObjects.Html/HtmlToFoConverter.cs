using System;
using Limbo.FormattingObjects.Graphics;
using Limbo.FormattingObjects.Html.Elements;
using Limbo.FormattingObjects.Inline;
using Limbo.FormattingObjects.Lists;
using Limbo.FormattingObjects.Styles;
using Skybrud.Essentials.Strings.Extensions;
using skybrudDk.Pdf.Html.Styles;

#pragma warning disable CS8632

namespace Limbo.FormattingObjects.Html;

public class HtmlToFoConverter : IHtmlToFoConverter {

    public virtual FoElement? Convert(HtmlNode html) {
        return html switch {
            HtmlAnchor anchor => ConvertElement(anchor),
            HtmlComment comment => ConvertComment(comment),
            HtmlDiv div => ConvertElement(div),
            HtmlEm em => ConvertElement(em),
            HtmlH2 h2 => ConvertElement(h2),
            HtmlH3 h3 => ConvertElement(h3),
            HtmlH4 h4 => ConvertElement(h4),
            HtmlH5 h5 => ConvertElement(h5),
            HtmlIframe iframe => ConvertElement(iframe),
            HtmlImg image => ConvertElement(image),
            HtmlLineBreak lineBreak => ConvertElement(lineBreak),
            HtmlOrderedList list => ConvertElement(list),
            HtmlParagraph paragraph => ConvertElement(paragraph),
            HtmlSpan span => ConvertElement(span),
            HtmlStrong strong => ConvertElement(strong),
            HtmlTable table => ConvertElement(table),
            HtmlUnorderedList list => ConvertElement(list),
            HtmlRoot root => ConvertElement(root),
            _ => throw new InvalidOperationException($"Unsupported HTML element '{html.GetType()}'.")
        };
    }

    protected virtual void ConvertChildren(HtmlElement html, FoContainer<FoNode> fo) {

        foreach (HtmlNode child in html.Children) {

            if (child is HtmlText text) {
                fo.Add(new FoText(text.Value.HtmlDecode()));
                continue;
            }

            FoNode? result = Convert(child);
            if (result is not null) fo.Add(result);

        }

    }

    protected virtual FoElement ConvertElement(HtmlAnchor element) {

        FoBasicLink link = new() { TextDecoration = FoTextDecoration.Underline, ExternalDestination = element.Href };

        ConvertChildren(element, link);

        return link;

    }

    protected virtual FoElement? ConvertComment(HtmlComment html) {
        return null;
    }

    protected virtual FoElement? ConvertElement(HtmlDiv html) {

        FoBlock fo = new();

        ConvertChildren(html, fo);

        return fo;

    }

    protected virtual FoElement ConvertElement(HtmlEm html) {

        FoInline fo = new() { FontStyle = FoFontStyle.Italic };

        ConvertChildren(html, fo);

        return fo;

    }

    protected virtual FoElement ConvertElement(HtmlH2 html) {

        FoBlock fo = new() {
            FontWeight = FoFontWeight.Bold,
            MarginTop = "15px",
            FontSize = "25px"
        };

        ConvertChildren(html, fo);

        return fo;

    }

    protected virtual FoElement ConvertElement(HtmlH3 html) {

        FoBlock fo = new() {
            FontWeight = FoFontWeight.Bold,
            MarginTop = "15px",
            FontSize = "22px"
        };

        ConvertChildren(html, fo);

        return fo;

    }

    protected virtual FoElement ConvertElement(HtmlH4 html) {

        FoBlock fo = new() {
            FontWeight = FoFontWeight.Bold,
            MarginTop = "10px",
            FontSize = "19px"
        };

        ConvertChildren(html, fo);

        return fo;

    }

    protected virtual FoElement ConvertElement(HtmlH5 html) {

        FoBlock fo = new() {
            FontWeight = FoFontWeight.Bold,
            MarginTop = "10px",
            FontSize = "16px"
        };

        ConvertChildren(html, fo);

        return fo;

    }

    protected virtual FoElement? ConvertElement(HtmlIframe element) {

        if (string.IsNullOrEmpty(element.Src)) return null;

        FoBasicLink link = new() {
            TextDecoration = FoTextDecoration.Underline,
            ExternalDestination = element.Src,
            FontSize = "13px",
            Color = "black"
        };

        link.Add(new FoBlock(element.Src));

        return link;

    }

    protected virtual FoElement? ConvertElement(HtmlImg element) {

        if (string.IsNullOrEmpty(element.Src)) return null;

        FoExternalGraphic gfx = new();
        if (element.Width > 0) gfx.ContentWidth = $"{element.Width}px";
        if (element.Height > 0) gfx.ContentHeight = $"{element.Height}px";

        if (element.Src.StartsWith("/")) {
            if ((element.Width > 0 || element.Height > 0) && element.Src.StartsWith("/media/")) {
                throw new NotImplementedException();
                //gfx.Src = GetAbsoluteUrl("/CropUp/" + element.Width + "x" + element.Height + element.Src);
            } else {
                gfx.Src = GetAbsoluteUrl(element.Src);
            }
        } else {
            gfx.Src = element.Src;
        }

        return gfx;

    }

    protected virtual FoElement? ConvertElement(HtmlLineBreak element) {
        return new FoBlock(" ");
    }

    protected virtual FoElement? ConvertElement(HtmlOrderedList element) {

        FoListBlock list = new() { MarginTop = "15px" };

        int n = 1;

        foreach (HtmlNode child in element.Children) {

            if (child is not HtmlListItem listItem) throw new InvalidOperationException($"Ordered list child must be of type '{typeof(HtmlListItem)}'.");

            FoListItem item = new() {
                MarginTop = "5px",
                Label = {
                    StartIndent = "15px"
                },
                Body = {
                    StartIndent = "35px"
                }
            };

            item.Label.Add(new FoText(n++ + "."));

            ConvertChildren(listItem, item.Body);

            list.Add(item);

        }

        return list;

    }

    protected virtual FoElement? ConvertElement(HtmlParagraph element) {

        FoBlock paragraph = new() { MarginTop = "10px" };

        ConvertChildren(element, paragraph);

        return paragraph;

    }

    protected virtual FoElement ConvertElement(HtmlSpan html) {

        FoInline fo = new();

        fo.FontWeight = html.FontWeight switch {
            HtmlFontWeight.Bold => FoFontWeight.Bold,
            HtmlFontWeight.Normal => FoFontWeight.Normal,
            _ => fo.FontWeight
        };

        fo.FontStyle = html.FontStyle switch {
            HtmlFontStyle.Italic => FoFontStyle.Italic,
            HtmlFontStyle.Normal => FoFontStyle.Normal,
            _ => fo.FontStyle
        };

        ConvertChildren(html, fo);

        return fo;

    }

    protected virtual FoElement ConvertElement(HtmlStrong html) {

        FoInline fo = new() { FontWeight = FoFontWeight.Bold };

        ConvertChildren(html, fo);

        return fo;

    }

    protected virtual FoElement ConvertElement(HtmlTable table) {
        throw new NotImplementedException();
    }

    protected virtual FoElement? ConvertElement(HtmlUnorderedList element) {

        FoListBlock list = new() { MarginTop = "15px" };

        foreach (HtmlNode child in element.Children) {

            if (child is not HtmlListItem listItem) throw new InvalidOperationException($"Ordered list child must be of type '{typeof(HtmlListItem)}'. Found '{child.GetType()}'");

            FoListItem item = new() {
                MarginTop = "5px",
                Label = new FoListItemLabel {
                    StartIndent = "15px"
                },
                Body = new FoListItemBody {
                    StartIndent = "35px"
                }
            };

            item.Label.Add(new FoText("•"));

            ConvertChildren(listItem, item.Body);

            list.Add(item);

        }

        return list;

    }

    protected virtual FoElement? ConvertElement(HtmlRoot root) {

        FoBlockContainer fo = new();

        ConvertChildren(root, fo);

        return fo;

    }

    protected virtual FoText ConvertText(HtmlText text) {
        return new FoText(text.Value.HtmlDecode());
    }

    protected virtual string GetAbsoluteUrl(string url) {
        if (url.StartsWith("/")) throw new NotImplementedException();
        return url;
    }

}