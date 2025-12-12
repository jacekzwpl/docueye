using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.WorkspacesKeeper.Model;


namespace DocuEye.WorkspacesKeeper.Application.Queries.GetViewConfiguration
{
    /// <summary>
    /// Gets workspace view configuration
    /// </summary>
    public class GetViewConfigurationQuery : IQuery<ViewConfiguration?>
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
