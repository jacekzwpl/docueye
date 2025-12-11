using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;


namespace DocuEye.WorkspaceImporter.Application.Commands.ImportViewConfiguration
{
    public class ImportViewConfigurationCommand : ICommand<ImportViewConfigurationResult>
    {
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
        public ViewConfigurationToImport ViewConfiguration { get; set; } = null!;
    }
}
