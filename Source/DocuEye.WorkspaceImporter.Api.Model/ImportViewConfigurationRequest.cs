using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportViewConfigurationRequest
    {
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
        public ViewConfigurationToImport ViewConfiguration { get; set; } = null!;
    }
}
