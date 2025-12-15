using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Application.Model;

using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementConsumers
{
    /// <summary>
    /// Gets element consumers
    /// </summary>
    public class GetElementConsumersQuery : IQuery<IEnumerable<ElementConsumer>>
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
        /// Indicates if linked dependencies should be returned
        /// </summary>
        public bool GetLinked { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID of the element</param>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="getLinked">Indicates if linked dependencies should be returned</param>
        public GetElementConsumersQuery(string id, string workspaceId, bool getLinked = false)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
            this.GetLinked = getLinked;
        }
    }
}
