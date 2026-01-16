using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterModelRelationship
    {
        
        public LinterModelElement Source { get; set; } = null!;
        public LinterModelElement Destination { get; set; } = null!;
        public string? Technology { get; set; } = null;
        public string? Description { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public List<string> Tags { get; set; } = new List<string>();

    }
}
