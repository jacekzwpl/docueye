using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Application.Model;

using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementDependences
{
    /// <summary>
    /// Gets element dependences
    /// </summary>
    public class GetElementDependencesQuery : IQuery<IEnumerable<ElementDependence>>
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
        /// Indicates if linked dependencies should be returned
        /// </summary>
        public bool GetLinked { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID fo the element</param>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="getLinked">Indicates if linked dependence's should be returned</param>
        public GetElementDependencesQuery(string id, string workspaceId, bool getLinked = false)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
            this.GetLinked = getLinked;
        }
    }

}
