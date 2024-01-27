using DocuEye.ViewsKeeper.Model;
using MediatR;

namespace DocuEye.ViewsKeeper.Application.Queries.GetComponentView
{
    /// <summary>
    /// Gets component view
    /// </summary>
    public class GetComponentViewQuery : IRequest<ComponentView?>
    {
        /// <summary>
        /// The view ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The view ID</param>
        /// <param name="workspaceId">The ID of workspace</param>
        public GetComponentViewQuery(string id, string workspaceId)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
        }
    }
}
