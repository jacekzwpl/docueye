using System;
using System.Collections.Generic;

namespace DocuEye.Linter.Model
{
    public class LinterModelElement
    {
         public string Identifier { get; set; } = null!;
         public string? ParentIdentifier { get; set; } = null;
         public string Name { get; set; } = null!;
         public IEnumerable<string>? Tags { get; set; }
         public string? Technology { get; set; }
         public string? Description { get; set; }
         public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
         public string? InstanceOfIdentifier { get; set; }

    }
}
