using DocuEye.DocsKeeper.Application.Commads.SaveSingleDecision;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.SaveSingleDecision
{
    public class SaveSingleDecisionCommandHandler : ICommandHandler<SaveSingleDecisionCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public SaveSingleDecisionCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task HandleAsync(SaveSingleDecisionCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Decisions.UpsertOneAsync(request.Decision);
        }
    }
}
