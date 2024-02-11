using AutoMapper;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementConsumers
{
    /// <summary>
    /// Handler for GetElementConsumersQuery
    /// </summary>
    public class GetElementConsumersQueryHandler : IRequestHandler<GetElementConsumersQuery, IEnumerable<ElementConsumer>>
    {
        private readonly IModelKeeperDBContext dbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        /// <param name="mapper">Automapper service</param>
        public GetElementConsumersQueryHandler(IModelKeeperDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of consumer elements</returns>
        public async Task<IEnumerable<ElementConsumer>> Handle(GetElementConsumersQuery request, CancellationToken cancellationToken)
        {
            var relationships = await this.dbContext.Relationships
                .Find(o => o.DestinationId == request.Id && o.WorkspaceId == request.WorkspaceId 
                && o.LinkedRelationshipId == null);
            var relationshipsId = relationships.Select(o => o.SourceId).ToArray();

            var elements = await this.dbContext.Elements
                .Find(o => relationshipsId.Contains(o.Id));

            var consumers = this.mapper.Map<IEnumerable<ElementConsumer>>(elements);
            //Fill relationships data for elements
            var usedRelationships = new List<string>();
            foreach (var consumer in consumers)
            {
                var relationship = relationships
                    .FirstOrDefault(o => o.SourceId == consumer.Id
                    && !usedRelationships.Contains(o.Id));
                if (relationship != null)
                {
                    consumer.RelationDescription = relationship.Description;
                    consumer.RelationTechnology = relationship.Technology;
                    usedRelationships.Add(relationship.Id);
                }
            }
            return consumers;
        }
    }
}
