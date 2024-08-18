using DocuEye.ModelKeeper.Model;
using MediatR;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId
{
    public class GetElementByStructurizrIdQuery : IRequest<Element?>
    {
        public string StructurizrId { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;

        public GetElementByStructurizrIdQuery(string structurizrId, string workspaceId)
        {
            StructurizrId = structurizrId;
            WorkspaceId = workspaceId;
        }
    }
}
