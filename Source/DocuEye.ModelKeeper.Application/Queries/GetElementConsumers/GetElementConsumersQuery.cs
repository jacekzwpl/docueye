using MediatR;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementConsumers
{
    /// <summary>
    /// Gets element consumers
    /// </summary>
    public class GetElementConsumersQuery : IRequest<IEnumerable<ElementConsumer>>
    {
        /// <summary>
        /// The ID of the element
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID of the element</param>
        /// <param name="workspaceId">The ID of workspace</param>
        public GetElementConsumersQuery(string id, string workspaceId)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
        }
    }
}
