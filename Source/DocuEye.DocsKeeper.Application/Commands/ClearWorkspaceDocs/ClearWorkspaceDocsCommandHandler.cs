using DocuEye.DocsKeeper.Application.Commads.ClearWorkspaceDocs;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.ClearWorkspaceDocs
{
    public class ClearWorkspaceDocsCommandHandler : IRequestHandler<ClearWorkspaceDocsCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public ClearWorkspaceDocsCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(ClearWorkspaceDocsCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Decisions.DeleteManyAsync(x => x.WorkspaceId == request.WorkspaceId);
            await this.dbContext.DocumentationFiles.DeleteManyAsync(x => x.WorkspaceId == request.WorkspaceId);
            await this.dbContext.Images.DeleteManyAsync(x => x.WorkspaceId == request.WorkspaceId);
            await this.dbContext.Documentations.DeleteManyAsync(x => x.WorkspaceId == request.WorkspaceId);
        }
    }

}
