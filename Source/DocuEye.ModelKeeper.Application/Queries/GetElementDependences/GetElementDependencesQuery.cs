using MediatR;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementDependences
{
    /// <summary>
    /// Gets element dependences
    /// </summary>
    public class GetElementDependencesQuery : IRequest<IEnumerable<ElementDependence>>
    {
        /// <summary>
        /// The ID fo the element
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID fo the element</param>
        /// <param name="workspaceId">The ID of workspace</param>
        public GetElementDependencesQuery(string id, string workspaceId)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
        }
    }

}
