using MediatR;

namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDecisions
{
    public class ClearDecisionsCommand : IRequest<ClearDecisionsResult>
    {
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;

        public ClearDecisionsCommand(string workspaceId, string importKey)
        {
            ImportKey = importKey;
            WorkspaceId = workspaceId;
        }
    }
}
