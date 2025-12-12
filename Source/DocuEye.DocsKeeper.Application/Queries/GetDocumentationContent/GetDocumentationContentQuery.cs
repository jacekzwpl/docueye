

using DocuEye.Infrastructure.Mediator.Queries;

namespace DocuEye.DocsKeeper.Application.Queries.GetWorkspaceDocumentation
{
    /// <summary>
    /// Gets documentation content for workspace or element
    /// </summary>
    public class GetDocumentationContentQuery : IQuery<DocumentationContent?>
    {
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Element Id
        /// </summary>
        public string? ElementId { get; set; }
        /// <summary>
        /// Host for download image uri
        /// </summary>
        public string BaseUrl { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="baseUrl">Host for download image uri</param>
        /// <param name="elementId">Element Id</param>
        public GetDocumentationContentQuery(string workspaceId, string baseUrl, string? elementId = null)
        {
            this.WorkspaceId = workspaceId;
            this.BaseUrl = baseUrl;
            this.ElementId = elementId;
        }
    }
}
