using DocuEye.DocsKeeper.Model;
using MediatR;

namespace DocuEye.DocsKeeper.Application.Queries.GetOpenApiFile
{
    public class GetOpenApiFileQuery : IRequest<DocumentationFile?>
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
