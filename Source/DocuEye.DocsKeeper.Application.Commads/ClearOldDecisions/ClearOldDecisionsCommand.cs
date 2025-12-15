

using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.DocsKeeper.Application.Commads.ClearOldDecisions
{
    public class ClearOldDecisionsCommand : ICommand
    {
        public string WorkspaceId { get; set; } = null!;
        public string ImportKey { get; set; } = null!;

        public ClearOldDecisionsCommand(string workspaceId, string importKey)
        {
            WorkspaceId = workspaceId;
            ImportKey = importKey;
        }
    }
}
