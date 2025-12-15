using DocuEye.DocsKeeper.Application.Commads.ClearDocumentations;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.ClearDocumentations
{
    public class ClearDocumentationsCommandHandler : ICommandHandler<ClearDocumentationsCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public ClearDocumentationsCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task HandleAsync(ClearDocumentationsCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Documentations.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
        }
    }
}
