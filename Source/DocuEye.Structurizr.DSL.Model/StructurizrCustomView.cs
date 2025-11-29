using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrCustomView
    {
        public string Identifier { get; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public IEnumerable<string>? Include { get; set; }
        public IEnumerable<IEnumerable<string>>? Animation { get; set; }
        public IEnumerable<string>? Exclude { get; set; }
        public StructurizrAutoLayout? AutomaticLayout { get; set; }
        public bool Default { get; set; } = false;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        public StructurizrCustomView(string identifier, string? title = null, string? description = null)
        {
            Title = title;
            Identifier = identifier;
            Description = description;
        }
    }
}
