using DocuEye.DocsKeeper.Application.Commads.SaveDocumentationChanges;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DocuEye.DocsKeeper.Application.Commads.SaveDecisions;
using System.Linq;

namespace DocuEye.DocsKeeper.Application.Commands.SaveDecisions
{
    /// <summary>
    /// Handler for SaveDecisionsCommand
    /// </summary>
    public class SaveDecisionsCommandHandler : IRequestHandler<SaveDecisionsCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongDB context</param>
        public SaveDecisionsCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles command
        /// </summary>
        /// <param name="request">command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task Handle(SaveDecisionsCommand request, CancellationToken cancellationToken)
        {
            //Delete all decisions
            await this.dbContext.Decisions.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
            // Create new decisions
            if (request.DecisionsToAdd.Count() > 0)
            {
                await this.dbContext.Decisions.InsertManyAsync(request.DecisionsToAdd);
            }

        }
    }
}
