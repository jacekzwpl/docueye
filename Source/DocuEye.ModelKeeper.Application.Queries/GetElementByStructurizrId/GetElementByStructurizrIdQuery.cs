using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Model;


namespace DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId
{
    public class GetElementByStructurizrIdQuery : IQuery<Element?>
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
