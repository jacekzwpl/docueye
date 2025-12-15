using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportElements
{
    public class ImportElementsCommand : ICommand<ImportElementsResult>
    {
        public IEnumerable<ElementToImport> Elements { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
    }
}
