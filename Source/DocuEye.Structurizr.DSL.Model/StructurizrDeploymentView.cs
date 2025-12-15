using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrDeploymentView
    {
        public string ElementIdentifier { get; set; }
        public string Environment { get; set; }
        public string Identifier { get; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public IEnumerable<string>? Include { get; set; }
        public IEnumerable<IEnumerable<string>>? Animation { get; set; }
        public IEnumerable<string>? Exclude { get; set; }
        public StructurizrAutoLayout? AutomaticLayout { get; set; }
        public bool Default { get; set; } = false;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public StructurizrDeploymentView(string elementIdentifier, string environment, string identifier, string? description = null)
        {
            ElementIdentifier = elementIdentifier;
            Identifier = identifier;
            Description = description;
            Environment = environment;
        }
    }
}
