using DocuEye.ViewsKeeper.Model;
using MediatR;

namespace DocuEye.ViewsKeeper.Application.Queries.GetSystemLandscapeView
{
    /// <summary>
    /// Gets System Landscape View
    /// </summary>
    public class GetSystemLandscapeViewQuery : IRequest<SystemLandscapeView?>
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
        public GetSystemLandscapeViewQuery(string id, string workspaceId) { 
            this.Id = id;
            this.WorkspaceId = workspaceId;
        }
    }
}
