using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.DocsKeeper.Application.Commads.DeleteDocumentationFile
{
    public class DeleteDocumentationFileCommand : ICommand
    {
        public string WorkspaceId { get; set; } = null!;
        public string ElementId { get; set; } = null!;
        public string DocumentationType { get; set; } = null!;

        public DeleteDocumentationFileCommand(string workspaceId, string elementId, string documentationType)
        {
            this.WorkspaceId = workspaceId;
            this.ElementId = elementId;
            this.DocumentationType = documentationType;
        }
    }
}
