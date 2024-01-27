using DocuEye.ModelKeeper.Model;
using MediatR;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships
{
    /// <summary>
    /// Gets all relationships in workspace
    /// </summary>
    public class GetAllWorkspaceRelationshipsQuery : IRequest<IEnumerable<Relationship>>
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        public GetAllWorkspaceRelationshipsQuery(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }

    }
}
