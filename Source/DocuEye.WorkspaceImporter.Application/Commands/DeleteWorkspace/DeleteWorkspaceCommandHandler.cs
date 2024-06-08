using DocuEye.ChangeTracker.Application.Commands.ClearWorkspaceEvents;
using DocuEye.DocsKeeper.Application.Commads.ClearWorkspaceDocs;
using DocuEye.ModelKeeper.Application.Commands.ClearWorkspaceModel;
using DocuEye.ViewsKeeper.Application.Commands.ClearWorkspaceViews;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application.Commands.ClearWorkspaceData;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand, DeleteWorkspaceResult>
    {
        private readonly IMediator mediator;
        private readonly IWorkspaceImporterDBContext dbContext;
        public DeleteWorkspaceCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
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

            //Views
            await this.mediator.Send(new ClearWorkspaceViewsCommand(request.WorkspaceId));

            //ModleChanges
            await this.mediator.Send(new ClearWorkspaceEventsCommand(request.WorkspaceId));

            //ViewConfigurations
            //Workspaces
            await this.mediator.Send(new ClearWorkspaceDataCommand(request.WorkspaceId));

            //WorkspaceImports
            await this.dbContext.WorkspaceImports.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
            return new DeleteWorkspaceResult();
        }
    }
}
