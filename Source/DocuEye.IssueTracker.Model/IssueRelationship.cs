using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.IssueTracker.Model
{
    public class IssueRelationship
    {
        public IssueElement Source { get; set; } = null!;
        public IssueElement Destination { get; set; } = null!;
    }
}
