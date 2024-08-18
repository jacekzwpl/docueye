using DocuEye.DocsKeeper.Model;
using MediatR;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisonByDslId
{
    public class GetDecisonByDslIdQuery : IRequest<Decision?>
    {
        public string WorkspaceId { get; set; }
        public string DocumentationId { get; set; }
        public string DslId { get; set; }

        public GetDecisonByDslIdQuery(string workspaceId, string documnetationId, string dslId)
        {
            this.WorkspaceId = workspaceId;
            this.DocumentationId = documnetationId;
            this.DslId = dslId;
        }
    }
}
