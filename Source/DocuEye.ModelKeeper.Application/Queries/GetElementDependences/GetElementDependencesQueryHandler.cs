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

namespace DocuEye.ModelKeeper.Application.Queries.GetElementDependences
{
    /// <summary>
    /// Handler for GetElementDependencesQuery
    /// </summary>
    public class GetElementDependencesQueryHandler : IQueryHandler<GetElementDependencesQuery, IEnumerable<ElementDependence>>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetElementDependencesQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of element dependences</returns>
        public async Task<IEnumerable<ElementDependence>> HandleAsync(GetElementDependencesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Relationship> relationships = Enumerable.Empty<Relationship>();
            if(!request.GetLinked)
            {
                relationships = await this.dbContext.Relationships
                .Find(o => o.SourceId == request.Id && o.WorkspaceId == request.WorkspaceId
                    && o.LinkedRelationshipId == null);
            }else
            {
                relationships = await this.dbContext.Relationships
                .Find(o => o.SourceId == request.Id && o.WorkspaceId == request.WorkspaceId);
            }

            var relationshipsId = relationships.Select(o => o.DestinationId).ToArray();

            var elements = await this.dbContext.Elements
                .Find(o => relationshipsId.Contains(o.Id));

            var dependences = elements.MapToElementDependences();
            var usedRelationships = new List<string>();
            foreach ( var dependence in dependences )
            {
                var relationship = relationships
                    .FirstOrDefault(o => o.DestinationId == dependence.Id 
                    && !usedRelationships.Contains(o.Id));
                if(relationship != null )
                {
                    dependence.RelationDescription = relationship.Description;
                    dependence.RelationTechnology = relationship.Technology;
                    usedRelationships.Add(relationship.Id);
                }
            }
            return dependences;
        }
    }
}
