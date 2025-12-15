using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrDynamicView
    {
        public string ElementIdentifier { get; set; }
        public string Identifier { get; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public StructurizrAutoLayout? AutomaticLayout { get; set; }
        public bool Default { get; set; } = false;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public List<StructurizrDynamicViewRelationship> Relationships { get; set; } = new List<StructurizrDynamicViewRelationship>();

        public StructurizrDynamicView(string elementIdentifier, string identifier, string? description = null)
        {
            ElementIdentifier = elementIdentifier;
            Identifier = identifier;
            Description = description;
        }
    }
}
