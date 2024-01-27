using DocuEye.WorkspacesKeeper.Model;
using MediatR;

namespace DocuEye.WorkspacesKeeper.Application.Queries.GetWorkspace
{
    /// <summary>
    /// Gets workspace
    /// </summary>
    public class GetWorkspaceQuery : IRequest<Workspace?>
    {
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID of workspace</param>
        public GetWorkspaceQuery(string id)
        {
            this.Id = id;
        }
    }
}
