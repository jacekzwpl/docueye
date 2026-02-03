using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Issues
{
    public class IssueElementToImport
    {
        public string Identifier { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
