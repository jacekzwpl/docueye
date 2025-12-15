using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrElementStyle
    {
        public string Tag { get; set; }
        public string? Shape { get; set; }
        public string? Icon { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? Background { get; set; }
        public string? Color { get; set; }
        public string? Stroke { get; set; }
        public int? StrokeWidth { get; set; }
        public int? FontSize { get; set; }
        public string? Border { get; set; }
        public int? Opacity { get; set; }
        public bool? Metadata { get; set; }
        public bool? Description { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public StructurizrElementStyle(string tag)
        {
            Tag = tag;
        }
    }
}
