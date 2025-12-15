using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Model;


namespace DocuEye.ViewsKeeper.Application.Queries.GetFilteredView
{
    /// <summary>
    /// Gets Filtered view
    /// </summary>
    public class GetFilteredViewQuery : IQuery<FilteredView?>
    {
        /// <summary>
        /// The ID of view
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID of view</param>
        /// <param name="workspaceId">The ID of workspace</param>
        public GetFilteredViewQuery(string id, string workspaceId)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
        }
    }
}
