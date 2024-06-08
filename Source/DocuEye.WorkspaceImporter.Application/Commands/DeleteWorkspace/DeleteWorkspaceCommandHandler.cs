using DocuEye.DocsKeeper.Application.Commads.ClearWorkspaceDocs;
using DocuEye.ModelKeeper.Application.Commands.ClearWorkspaceModel;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand, DeleteWorkspaceResult>
    {
        private readonly IMediator mediator;
        public DeleteWorkspaceCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<DeleteWorkspaceResult> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            //Relationships
            //Elements
            await this.mediator.Send(new ClearWorkspaceModelCommand(request.WorkspaceId));

            //Decisions
            //DocumentationFiles
            //Documentaions
            //Images
            await this.mediator.Send(new ClearWorkspaceDocsCommand(request.WorkspaceId));


            //ModleChanges
            
            //ViewConfigurations
            //Views
            //WorkspaceImoorts
            //Workspaces


            throw new System.NotImplementedException();
        }
    }
}
