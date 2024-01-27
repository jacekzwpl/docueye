using DocuEye.ModelKeeper.Model;
using MediatR;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements
{
    /// <summary>
    /// Gets all elements in workspace
    /// </summary>
    public class GetAllWorkspaceElementsQuery : IRequest<IEnumerable<Element>>
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        public GetAllWorkspaceElementsQuery(string workspaceId) {  
            this.WorkspaceId = workspaceId; 
        }

    }
}
