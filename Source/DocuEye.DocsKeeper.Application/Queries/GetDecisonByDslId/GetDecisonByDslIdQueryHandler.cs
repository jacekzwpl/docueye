using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisonByDslId
{
    public class GetDecisonByDslIdQueryHandler : IRequestHandler<GetDecisonByDslIdQuery, Decision?>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public GetDecisonByDslIdQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Decision?> Handle(GetDecisonByDslIdQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Decisions
                .FindOne(o => o.WorkspaceId == request.WorkspaceId 
                && o.DocumentationId == request.DocumentationId
                && o.DslId == request.DslId);
        }
    }
}
