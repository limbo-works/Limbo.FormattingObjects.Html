using System;

namespace Limbo.FormattingObjects.Html.Elements;

public class HtmlAttribute {

    public string Name { get; internal set; }

    public string Value { get; set; }

    [Obsolete("Use constructor overload instead.")]
    public HtmlAttribute() { }

    public HtmlAttribute(string name, string value) {
        Name = name;
        Value = value;
    }

}