using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Application.Model;

using System.Collections.Generic;

namespace DocuEye.ViewsKeeper.Application.Queries.FindViewsWithElement
{
    /// <summary>
    /// Find all views that contains specyfic element
    /// </summary>
    public class FindViewsWithElementQuery : IQuery<IEnumerable<ViewWithElement>>
    {
        /// <summary>
        /// The ID of element
        /// </summary>
        public string ElementId { get; set; }
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="elementId">The ID of element</param>
        /// <param name="workspaceId">The ID of workspace</param>
        public FindViewsWithElementQuery(string elementId, string workspaceId)
        {
            this.ElementId = elementId;
            this.WorkspaceId = workspaceId;
        }
    }
}
