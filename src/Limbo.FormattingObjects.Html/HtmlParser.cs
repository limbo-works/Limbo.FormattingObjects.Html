using System;
using HtmlAgilityPack;
using Limbo.FormattingObjects.Html.Elements;
using HtmlNode = Limbo.FormattingObjects.Html.Elements.HtmlNode;

#pragma warning disable CS8632

namespace Limbo.FormattingObjects.Html;

public class HtmlParser : IHtmlParser {

    public virtual HtmlElement Parse(string html) {

        HtmlDocument doc = new();
        doc.LoadHtml($"<html>{html}</html>");

        return ParseNode(doc.DocumentNode.FirstChild) as HtmlElement;

    }

    public virtual HtmlNode? ParseNode(HtmlAgilityPack.HtmlNode node) {
        return node.Name switch {
            "a" => ParseAnchor(node),
            "b" => ParseStrong(node),
            "br" => ParseLineBreak(node),
            "div" => ParseDiv(node),
            "i" => ParseEm(node),
            "em" => ParseEm(node),
            "html" => ParseHtml(node),
            "li" => ParseListItem(node),
            "p" => ParseParagraph(node),
            "span" => ParseSpan(node),
            "strong" => ParseStrong(node),
            "ul" => ParseUnorderedList(node),
            "#text" => ParseText(node),
            _ => throw new InvalidOperationException($"Unsupported element '{node.Name}'.")
        };
    }

    protected virtual void ParseAttributes(HtmlAgilityPack.HtmlNode node, HtmlElement element) {

        foreach (HtmlAgilityPack.HtmlAttribute attr in node.Attributes) {
            element.SetAttributeValue(attr.Name, attr.Value);
        }

    }

    protected virtual void ParseChildren(HtmlAgilityPack.HtmlNode parentNode, HtmlElement parentElement) {

        foreach (HtmlAgilityPack.HtmlNode child in parentNode.ChildNodes) {

            HtmlNode node = ParseNode(child);
            if (node is null) continue;

            parentElement.AppendChild(node);

        }

    }

    protected virtual HtmlAnchor ParseAnchor(HtmlAgilityPack.HtmlNode node) {
        HtmlAnchor anchor = new();
        ParseAttributes(node, anchor);
        ParseChildren(node, anchor);
        return anchor;
    }

    protected virtual HtmlDiv ParseDiv(HtmlAgilityPack.HtmlNode node) {
        HtmlDiv element = new();
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlEm ParseEm(HtmlAgilityPack.HtmlNode node) {
        HtmlEm element = new(node);
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlRoot ParseHtml(HtmlAgilityPack.HtmlNode node) {
        HtmlRoot element = new(node);
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlListItem ParseListItem(HtmlAgilityPack.HtmlNode node) {
        HtmlListItem element = new();
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlLineBreak ParseLineBreak(HtmlAgilityPack.HtmlNode node) {
        HtmlLineBreak element = new();
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlParagraph ParseParagraph(HtmlAgilityPack.HtmlNode node) {
        HtmlParagraph element = new(node);
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlUnorderedList ParseUnorderedList(HtmlAgilityPack.HtmlNode node) {
        HtmlUnorderedList element = new(node);
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlSpan ParseSpan(HtmlAgilityPack.HtmlNode node) {
        HtmlSpan element = new(node);
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlStrong ParseStrong(HtmlAgilityPack.HtmlNode node) {
        HtmlStrong element = new();
        ParseAttributes(node, element);
        ParseChildren(node, element);
        return element;
    }

    protected virtual HtmlText? ParseText(HtmlAgilityPack.HtmlNode node) {
        string text = node.InnerText.Trim();
        return string.IsNullOrWhiteSpace(text) ? null : new HtmlText(node, text);
    }

}