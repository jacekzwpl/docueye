using DocuEye.DocsKeeper.Application.Commads.ClearDocumentations;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.ClearDocumentations
{
    public class ClearDocumentationsCommandHandler : IRequestHandler<ClearDocumentationsCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public ClearDocumentationsCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(ClearDocumentationsCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Documentations.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
        }
    }
}
