using MediatR;

namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDocsItems
{
    public class ClearDocsItemsCommand : IRequest<ClearDocsItemsResult>
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
