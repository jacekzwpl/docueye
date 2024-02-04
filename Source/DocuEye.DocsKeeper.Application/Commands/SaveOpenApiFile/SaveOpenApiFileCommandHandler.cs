using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.SaveOpenApiFile
{
    public class SaveOpenApiFileCommandHandler : IRequestHandler<SaveOpenApiFileCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongDB context</param>
        public SaveOpenApiFileCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task Handle(SaveOpenApiFileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
