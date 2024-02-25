using MediatR;
using System.Collections.Generic;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisionsList
{
    public class GetDecisionsListQuery : IRequest<IEnumerable<DecisionsListItem>>
    {
        /// <summary>
        /// Workspace id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Element id
        /// </summary>
        public string? ElementId { get; set; }

        public GetDecisionsListQuery(string worspaceId, string? elementId = null)
        {
            this.WorkspaceId = worspaceId;
            this.ElementId = elementId;
        }
    }
}
