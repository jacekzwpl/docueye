using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Application.Model;

using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetChildElements
{
    /// <summary>
    /// Gets elements children 
    /// </summary>
    public class GetChildElementsQuery : IQuery<IEnumerable<ChildElement>>
    {
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// The ID of element that children will be returned
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// Type of child elements that will be returned
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="parentId">The ID of element that children will be returned</param>
        /// <param name="type">Type of child elements that will be returned</param>
        public GetChildElementsQuery(string workspaceId, string parentId, string? type = null)
        {
            this.WorkspaceId = workspaceId;
            this.ParentId = parentId;
            this.Type = type;
        }
    }
}
