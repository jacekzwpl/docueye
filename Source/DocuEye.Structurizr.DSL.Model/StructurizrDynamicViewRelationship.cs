using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrDynamicViewRelationship
    {
        public string Identifier { get; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int Order { get; set; }
        public string? Description { get; set; }
        public string? Technology { get; set; }

        public StructurizrDynamicViewRelationship(string identifier, string source, string destination, int order, string? description = null, string? technology = null)
        {
            Identifier = identifier;
            Source = source;
            Destination = destination;
            Order = order;
            Description = description;
            Technology = technology;
        }
    }
}
