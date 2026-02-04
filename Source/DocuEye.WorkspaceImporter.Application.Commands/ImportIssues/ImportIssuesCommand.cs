using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.Issues;
using System.Collections.Generic;


namespace DocuEye.WorkspaceImporter.Application.Commands.ImportIssues
{
    public class ImportIssuesCommand : ICommand<ImportIssuesResult>
    {
        public string WorkspaceId { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public IEnumerable<IssueToImport> Issues { get; set; } = null!;

        public ImportIssuesCommand(string workspaceId, string importKey, IEnumerable<IssueToImport> issues)
        {
            WorkspaceId = workspaceId;
            ImportKey = importKey;
            Issues = issues;
        }
    }
}
