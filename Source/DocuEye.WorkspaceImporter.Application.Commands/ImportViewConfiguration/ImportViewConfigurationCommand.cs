using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using MediatR;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportViewConfiguration
{
    public class ImportViewConfigurationCommand : IRequest<ImportViewConfigurationResult>
    {
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
        public ViewConfigurationToImport ViewConfiguration { get; set; } = null!;
    }
}
