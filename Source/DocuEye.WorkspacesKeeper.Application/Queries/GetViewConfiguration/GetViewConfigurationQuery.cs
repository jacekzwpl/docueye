using DocuEye.WorkspacesKeeper.Model;
using MediatR;

namespace DocuEye.WorkspacesKeeper.Application.Queries.GetViewConfiguration
{
    /// <summary>
    /// Gets workspace view configuration
    /// </summary>
    public class GetViewConfigurationQuery : IRequest<ViewConfiguration?>
    {
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        public GetViewConfigurationQuery(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }
    }
}
