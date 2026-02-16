using DocuEye.WorkspaceImporter.Api.Model.Issues;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportIssuesRequest
    {
        public string WorkspaceId { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public IEnumerable<IssueToImport> Issues { get; set; } = null!;
    }
}
