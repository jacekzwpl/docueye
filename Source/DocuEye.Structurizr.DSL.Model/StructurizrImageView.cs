using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrImageView
    {
        public string ElementIdentifier { get; set; }
        public string Identifier { get; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public bool Default { get; set; } = false;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public string? ImageSource { get; set; }
        public string? PlantUMLSource { get; set; }
        public string? MermaidSource { get; set; }
        public StructurizrKrokiConfig? KrokiConfig { get; set; }

        public StructurizrImageView(string elementIdentifier, string identifier)
        {
            ElementIdentifier = elementIdentifier;
            Identifier = identifier;
        }
    }
}
