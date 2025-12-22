using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetViewLayout
{
    public class GetViewLayoutQueryHandler : IQueryHandler<GetViewLayoutQuery, ViewLayout?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        public GetViewLayoutQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<ViewLayout?> HandleAsync(GetViewLayoutQuery query, CancellationToken cancellationToken = default)
        {
            return await this.dbContext.ViewLayouts
                .FindOne(o => o.Id == query.ViewId && o.WorkspaceId == query.WorkspaceId);
        }
    }
}
