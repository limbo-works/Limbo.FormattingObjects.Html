using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8632

namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlAttributeList {

    private readonly Dictionary<string, HtmlAttribute> _attributes = new();

    #region Properties

    /// <summary>
    /// Gets or sets the ID of the parent element.
    /// </summary>
    public string? Id {
        get { return this["id"]; }
        set { this["id"] = value; }
    }

    /// <summary>
    /// Gets or sets a list representing the individual class names of the parent element.
    /// </summary>
    public IReadOnlyList<string> Classes {
        get { return this["class"]?.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>(); }
        set { this["class"] = value is null || value.Count is 0 ? null : string.Join(" ", value); }
    }

    /// <summary>
    /// Gets or sets the value of the attribute with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name">Thje name of the attribute.</param>
    /// <returns>The attribute value.</returns>
    public string? this[string name] {
        get => _attributes.TryGetValue(name, out HtmlAttribute? attr) ? attr!.Value : null;
        set {
            if (_attributes.TryGetValue(name, out HtmlAttribute? attr)) {
                attr!.Value = value;
            } else {
                _attributes.Add(name, new HtmlAttribute(name, value));
            }
        }
    }

    #endregion

    #region Member methods

    public bool HasAttribute(string name) {
        return _attributes.ContainsKey(name);
    }

    public HtmlAttribute? GetAttribute(string name) {
        return _attributes.TryGetValue(name, out HtmlAttribute? attr) ? attr : null;
    }

    public string? GetAttributeValue(string name) {
        return this[name];
    }

    public void AddClass(string className) {
        if (Classes.Contains(className)) return;
        List<string> classList = new(Classes) { className };
        Classes = classList;
    }

    public bool HasClass(string className) {
        return Classes.Contains(className);
    }

    public void AddClasses(string[] classNames) {
        foreach (string className in classNames) {
            AddClass(className);
        }
    }

    public void RemoveClass(string className) {
        Classes = (from name in Classes where name != className select name).ToList();
    }

    #endregion

}