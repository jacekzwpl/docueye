using DocuEye.ModelKeeper.Model;
using MediatR;

namespace DocuEye.ModelKeeper.Application.Queries.GetElement
{
    /// <summary>
    /// Gets element data
    /// </summary>
    public class GetElementQuery : IRequest<Element?>
    {
        /// <summary>
        /// The ID of Element
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID of Element</param>
        /// <param name="workspaceId">Workspace Id</param>
        public GetElementQuery(string id, string workspaceId)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
        }
    }
}
