using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Queries.GetOpenApiFile
{
    public class GetOpenApiFileQueryHandler : IRequestHandler<GetOpenApiFileQuery, DocumentationFile?>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetOpenApiFileQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<DocumentationFile?> Handle(GetOpenApiFileQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
