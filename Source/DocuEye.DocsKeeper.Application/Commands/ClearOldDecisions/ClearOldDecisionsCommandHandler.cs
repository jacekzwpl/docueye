using DocuEye.DocsKeeper.Application.Commads.ClearOldDecisions;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.ClearOldDecisions
{
    public class ClearOldDecisionsCommandHandler : IRequestHandler<ClearOldDecisionsCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public ClearOldDecisionsCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(ClearOldDecisionsCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Decisions
                .DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId && o.ImportKey != request.ImportKey);
        }
    }
}
