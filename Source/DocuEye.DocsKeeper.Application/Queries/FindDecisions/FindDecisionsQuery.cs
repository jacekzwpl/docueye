using DocuEye.DocsKeeper.Application.Model;
using MediatR;
using System.Collections.Generic;

namespace DocuEye.DocsKeeper.Application.Queries.FindDecisions
{
    /// <summary>
    /// Finds decisions for workspace or element
    /// </summary>
    public class FindDecisionsQuery : IRequest<IEnumerable<FoundedDecision>>
    {
        /// <summary>
        /// Workspace id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Element id
        /// </summary>
        public string? ElementId { get; set; }
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
        /// <param name="workspaceId">Workspace id</param>
        /// <param name="elementId">Element id</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        public FindDecisionsQuery(string workspaceId, string? elementId = null, int? limit = null, int? skip = null)
        {
            WorkspaceId = workspaceId;
            ElementId = elementId;
            Limit = limit;
            Skip = skip;
        }
    }
}
