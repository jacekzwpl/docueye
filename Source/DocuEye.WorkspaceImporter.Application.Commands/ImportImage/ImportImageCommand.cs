using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.Docs;


namespace DocuEye.WorkspaceImporter.Application.Commands.ImportImage
{
    public class ImportImageCommand : ICommand<ImportImageResult>
    {
        public string WorkspaceId { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public ImageToImport Image { get; set; } = null!;

        public ImportImageCommand(string workspaceId, string importKey, ImageToImport image)
        {
            WorkspaceId = workspaceId;
            ImportKey = importKey;
            Image = image;
        }
    }
}
