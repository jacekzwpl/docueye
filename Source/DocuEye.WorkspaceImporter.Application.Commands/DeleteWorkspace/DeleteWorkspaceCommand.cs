using MediatR;

namespace DocuEye.WorkspaceImporter.Application.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceCommand : IRequest<DeleteWorkspaceResult>
    {
        public string WorkspaceId { get; set; } = null!;
    }
}
