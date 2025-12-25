using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterModelRelationship
    {
        
        public LinterModelElement Source { get; set; }
        public LinterModelElement Destination { get; set; }
        public string? Technology { get; set; } = null;
    }
}
