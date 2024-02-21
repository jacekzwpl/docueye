﻿using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetAllViews
{
    public class GetAllViewsQueryHandler : IRequestHandler<GetAllViewsQuery, IEnumerable<BaseView>>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetAllViewsQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<BaseView>> Handle(GetAllViewsQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.AllViews
                    .Find(o => o.WorkspaceId == request.WorkspaceId);
        }
    }
}