using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrFilteredView
    {
        public string Identifier { get; }
        public string BaseViewIdentifier { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string FilterMode { get; set; } = "include";
        public List<string> Tags { get; set; }
        public bool Default { get; set; } = false;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public StructurizrFilteredView(string identifier, string baseViewIdentifier)
        {
            Identifier = identifier;
            BaseViewIdentifier = baseViewIdentifier;
            Tags = new List<string>();
        }
    }
}
