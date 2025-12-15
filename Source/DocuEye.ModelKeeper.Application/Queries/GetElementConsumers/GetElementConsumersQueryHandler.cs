using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Application.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Model.Maps;
using DocuEye.ModelKeeper.Persistence;

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
    public class GetElementConsumersQueryHandler : IQueryHandler<GetElementConsumersQuery, IEnumerable<ElementConsumer>>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetElementConsumersQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of consumer elements</returns>
        public async Task<IEnumerable<ElementConsumer>> HandleAsync(GetElementConsumersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Relationship> relationships = Enumerable.Empty<Relationship>();
            if (!request.GetLinked)
            {
                relationships = await this.dbContext.Relationships
                .Find(o => o.DestinationId == request.Id && o.WorkspaceId == request.WorkspaceId
                && o.LinkedRelationshipId == null);
            }
            else
            {
                relationships = await this.dbContext.Relationships
                .Find(o => o.DestinationId == request.Id && o.WorkspaceId == request.WorkspaceId);
            }

            var relationshipsId = relationships.Select(o => o.SourceId).ToArray();

            var elements = await this.dbContext.Elements
                .Find(o => relationshipsId.Contains(o.Id));

            var consumers = elements.MapToElementConsumers();
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
