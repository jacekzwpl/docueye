using MediatR;

namespace DocuEye.ModelKeeper.Application.Commands.ClearWorkspaceModel
{
    public class ClearWorkspaceModelCommand : IRequest
    {
        public string WorkspaceId { get; set; } = null!;

        public ClearWorkspaceModelCommand(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
