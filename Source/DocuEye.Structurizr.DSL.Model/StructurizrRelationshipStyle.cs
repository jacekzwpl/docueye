using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrRelationshipStyle
    {
        public string Tag { get; set; }
        public int? Thickness { get; set; }
        public string? Color { get; set; }
        public string? Style { get; set; }
        public string? Routing { get; set; }
        public int? FontSize { get; set; }
        public int? Width { get; set; }
        public int? Opacity { get; set; }
        public int? Position { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public StructurizrRelationshipStyle(string tag) { 
            this.Tag = tag;
        }
    }
}
