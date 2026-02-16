using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Issues
{
    public class IssueRelationshipToImport
    {
        public string Identifier { get; set; } = null!;
        public IssueElementToImport Source { get; set; } = null!;
        public IssueElementToImport Destination { get; set; } = null!;
    }
}
