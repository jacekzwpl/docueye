using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DocuEye.DocsKeeper.Application.Commads.SaveDocumentationChanges;
using DocuEye.DocsKeeper.Persistence;
using System.Linq;

namespace DocuEye.DocsKeeper.Application.Commands.SaveDocumentation
{
    /// <summary>
    /// Handler for SaveDocumentationCommand
    /// </summary>
    public class SaveDocumentationCommandHandler : IRequestHandler<SaveDocumentationCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongDB context</param>
        public SaveDocumentationCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles command
        /// </summary>
        /// <param name="request">command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task Handle(SaveDocumentationCommand request, CancellationToken cancellationToken)
        {
            //Delete all documentations
            await this.dbContext.Documentations.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
            // Create new documentations
            if (request.DocumentationsToAdd.Count() > 0)
            {
                await this.dbContext.Documentations.InsertManyAsync(request.DocumentationsToAdd);
            }

        }
    }
}
