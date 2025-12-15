using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.WorkspacesKeeper.Application.Model;

using System.Collections.Generic;

namespace DocuEye.WorkspacesKeeper.Application.Queries.FindWorspaces
{
    /// <summary>
    /// Gets list of workspaces
    /// </summary>
    public class FindWorkspacesQuery : IQuery<IEnumerable<FoundedWorkspace>>
    {
        /// <summary>
        /// Workspace name filter
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Maximum number of items that will be returned
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// Number of items to skip<
        /// </summary>
        public int? Skip { get; set; }
        public string? UserName { get; set; }
    }
}
