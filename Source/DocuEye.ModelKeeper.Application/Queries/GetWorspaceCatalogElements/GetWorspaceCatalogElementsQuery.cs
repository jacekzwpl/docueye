using DocuEye.ModelKeeper.Application.Model;
using MediatR;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetWorspaceCatalogElements
{
    /// <summary>
    /// Gets elements for workspace
    /// </summary>
    public class GetWorspaceCatalogElementsQuery : IRequest<IEnumerable<WorkspaceCatalogElement>>
    {
        /// <summary>
        /// The ID fo the workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Element name filter
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Element type filter
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Maximum number of items that will be returned
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// Number of items to skip
        /// </summary>
        public int? Skip { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">The ID fo the workspace</param>
        /// <param name="name">Element name filter</param>
        /// <param name="type">Element type filter</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        public GetWorspaceCatalogElementsQuery(string workspaceId, string? name = null, string? type = null, int? limit = null, int? skip = null)
        {
            this.WorkspaceId = workspaceId; 
            this.Name = name;
            this.Type = type;
            this.Limit = limit;
            this.Skip = skip;
        }
    }
}
