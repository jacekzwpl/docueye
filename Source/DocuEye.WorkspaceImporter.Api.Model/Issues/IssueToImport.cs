using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Issues
{
    public class IssueToImport
    {
        public string? Message { get; set; }
        public int SeverityValue { get; set; }
        public IssueElementToImport? Element { get; set; } = null;
        public IssueRelationshipToImport? Relationship { get; set; } = null;
        public IssueRuleToImport Rule { get; set; } = null!;
    }
}
