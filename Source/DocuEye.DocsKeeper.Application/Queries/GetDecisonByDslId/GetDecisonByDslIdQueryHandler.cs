using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisonByDslId
{
    public class GetDecisonByDslIdQueryHandler : IQueryHandler<GetDecisonByDslIdQuery, Decision?>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public GetDecisonByDslIdQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Decision?> HandleAsync(GetDecisonByDslIdQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Decisions
                .FindOne(o => o.WorkspaceId == request.WorkspaceId 
                && o.DocumentationId == request.DocumentationId
                && o.DslId == request.DslId);
        }
    }
}
