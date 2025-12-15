using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator.Queries;


namespace DocuEye.DocsKeeper.Application.Queries.GetOpenApiFile
{
    public class GetOpenApiFileQuery : IQuery<DocumentationFile?>
    {
        public string WorkspaceId { get; set; }
        public string ElementId { get; set; }

        public GetOpenApiFileQuery(string workspaceId, string elementId)
        {
            this.WorkspaceId = workspaceId;
            this.ElementId = elementId;
        }
    }
}
