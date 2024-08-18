using DocuEye.DocsKeeper.Application.Commads.SaveSingleDocumentation;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.SaveSingleDocumentation
{
    public class SaveSingleDocumentationCommandHandler : IRequestHandler<SaveSingleDocumentationCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public SaveSingleDocumentationCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(SaveSingleDocumentationCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Documentations.InsertOneAsync(request.Documentation);
        }
    }
}
