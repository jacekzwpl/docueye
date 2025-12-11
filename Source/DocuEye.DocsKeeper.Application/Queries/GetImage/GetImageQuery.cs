using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator.Queries;


namespace DocuEye.DocsKeeper.Application.Queries.GetImage
{
    /// <summary>
    /// Gets the image for display
    /// </summary>
    public class GetImageQuery : IQuery<Image?>
    {
        /// <summary>
        /// Documentation Id
        /// </summary>
        public string DocumentationId { get; set; }
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Name of the image
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="documentationId">Documentation Id</param>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="name">Name of the image</param>
        public GetImageQuery(string documentationId, string workspaceId, string name)
        {
            this.DocumentationId = documentationId;
            this.WorkspaceId = workspaceId;
            this.Name = name;
        }
    }
}
