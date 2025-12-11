

using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDocsItems
{
    public class ClearDocsItemsCommand : ICommand<ClearDocsItemsResult>
    {
        public string WorkspaceId { get; set; } = null!;

        public string ImportKey { get; set; } = null!;

        public ClearDocsItemsCommand(string workspaceId, string importKey)
        {
            WorkspaceId = workspaceId;
            ImportKey = importKey;
        }
    }
}
