using AutoMapper;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
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
    public class GetElementDependencesQueryHandler : IRequestHandler<GetElementDependencesQuery, IEnumerable<ElementDependence>>
    {
        private readonly IModelKeeperDBContext dbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        /// <param name="mapper">Automapper service</param>
        public GetElementDependencesQueryHandler(IModelKeeperDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of element dependences</returns>
        public async Task<IEnumerable<ElementDependence>> Handle(GetElementDependencesQuery request, CancellationToken cancellationToken)
        {
            var relationships = await this.dbContext.Relationships
                .Find(o => o.SourceId == request.Id && o.WorkspaceId == request.WorkspaceId);
            var relationshipsId = relationships.Select(o => o.DestinationId).ToArray();

            var elements = await this.dbContext.Elements
                .Find(o => relationshipsId.Contains(o.Id));
            
            var dependences = this.mapper.Map<IEnumerable<ElementDependence>>(elements);
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
