using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

#pragma warning disable CS8632

namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlElement : HtmlNode {

    private readonly List<HtmlNode> _children;

    [JsonIgnore]
    public HtmlAgilityPack.HtmlNode AgilityNode { get; internal set; }

    public string Name { get; }

    public HtmlAttributeList Attributes { get; internal set; }

    public IReadOnlyList<HtmlNode> Nodes => _children;

    public IReadOnlyList<HtmlNode> Children => _children;

    public string? Id {
        get { return Attributes.Id; }
    }

    public IReadOnlyList<string> Classes {
        get { return Attributes.Classes; }
    }

    public HtmlElement(string name) {
        Name = name;
        Attributes = new HtmlAttributeList();
        _children = new List<HtmlNode>();
    }

    public HtmlElement(HtmlAgilityPack.HtmlNode node) {
        AgilityNode = node;
        Name = node.Name;
        Attributes = new HtmlAttributeList();
        _children = new List<HtmlNode>();
    }

    public HtmlElement(HtmlAgilityPack.HtmlNode node, HtmlElement parent = null) {
        AgilityNode = node;
        Parent = parent;
        Name = node.Name;
        Attributes = new HtmlAttributeList();
        _children = new List<HtmlNode>();
    }

    public HtmlElement(HtmlAgilityPack.HtmlNode node, HtmlAttributeList attributes, HtmlElement parent = null) {
        AgilityNode = node;
        Attributes = attributes;
        Parent = parent;
        Name = node.Name;
        _children = new List<HtmlNode>();
    }

    public bool HasAttribute(string name) {
        return Attributes.HasAttribute(name);
    }

    public HtmlAttribute? Attribute(string name) {
        return Attributes.GetAttribute(name);
    }

    public string? AttributeValue(string name) {
        return Attributes.GetAttributeValue(name);
    }

    public void SetAttributeValue(string name, string? value) {
        Attributes[name] = value;
    }

    public void AppendChild(HtmlNode child) {

        // Make sure we set the parent element
        child.Parent = this;

        // Get the previous node (if any)
        HtmlNode prev = Nodes.LastOrDefault();

        // Update the previous/next relation
        if (prev is not null) {
            child.Previous = prev;
            prev.Next = child;
        }

        // And finally append the child to the list of children
        _children.Add(child);

    }

    public HtmlElement Element(string name) {
        return Nodes.OfType<HtmlElement>().FirstOrDefault(x => x.Name == name);
    }

    public IEnumerable<HtmlElement> Elements(string name) {
        return Nodes.OfType<HtmlElement>().Where(x => x.Name == name);
    }

    public IEnumerable<HtmlElement> Elements() {
        return Nodes.OfType<HtmlElement>();
    }

    public IEnumerable<HtmlElement> Elements(Func<HtmlElement, bool> func) {
        return Nodes.OfType<HtmlElement>().Where(func);
    }

    public HtmlElement Element(Func<HtmlElement, bool> func) {
        return Nodes.OfType<HtmlElement>().FirstOrDefault(func);
    }

    public HtmlElement ElementAt(int index) {
        return Nodes.OfType<HtmlElement>().ElementAt(index);
    }

    public bool HasElements() {
        return Nodes.OfType<HtmlElement>().Any();
    }

    public bool HasClass(string name) {
        return Classes.Any(x => x == name);
    }

    public void AddClass(string className) {
        Attributes.AddClass(className);
    }

    public void AddClasses(string[] classNames) {
        Attributes.AddClasses(classNames);
    }

    public void RemoveClass(string className) {
        Attributes.RemoveClass(className);
    }

}