using MediatR;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisionContent
{
    /// <summary>
    /// Gets content for decision
    /// </summary>
    public class GetDecisionQuery : IRequest<DecisionContent?>
    {
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Decision Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Host for download image uri
        /// </summary>
        public string BaseUrl { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace id</param>
        /// <param name="id">Decision Id</param>
        /// <param name="baseUrl">Host for download image uri</param>
        public GetDecisionQuery(string workspaceId, string id, string baseUrl)
        {
            this.Id = id;
            this.WorkspaceId = workspaceId;
            this.BaseUrl = baseUrl;
        }
    }
}
