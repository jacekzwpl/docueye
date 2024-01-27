﻿using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships
{
    /// <summary>
    /// Handler for GetAllWorkspaceRelationshipsQuery
    /// </summary>
    public class GetAllWorkspaceRelationshipsQueryHandler : IRequestHandler<GetAllWorkspaceRelationshipsQuery, IEnumerable<Relationship>>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetAllWorkspaceRelationshipsQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of relationships</returns>
        public async Task<IEnumerable<Relationship>> Handle(GetAllWorkspaceRelationshipsQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Relationships
                .Find(o => o.WorkspaceId == request.WorkspaceId);

        }
    }
}
