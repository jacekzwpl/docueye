using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Model;

using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships
{
    /// <summary>
    /// Gets all relationships in workspace
    /// </summary>
    public class GetAllWorkspaceRelationshipsQuery : IQuery<IEnumerable<Relationship>>
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
