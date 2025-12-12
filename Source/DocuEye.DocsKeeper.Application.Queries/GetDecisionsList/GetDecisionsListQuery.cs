using DocuEye.DocsKeeper.Application.Model;
using DocuEye.Infrastructure.Mediator.Queries;
using System.Collections.Generic;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisionsList
{
    public class GetDecisionsListQuery : IQuery<IEnumerable<DecisionsListItem>>
    {
        /// <summary>
        /// Workspace id
        /// </summary>
        public string WorkspaceId { get; set; }

        public GetDecisionsListQuery(string worspaceId)
        {
            this.WorkspaceId = worspaceId;
        }
    }
}
