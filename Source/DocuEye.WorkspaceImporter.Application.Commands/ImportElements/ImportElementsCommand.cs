using DocuEye.WorkspaceImporter.Api.Model;
using MediatR;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportElements
{
    public class ImportElementsCommand : IRequest<ImportElementsResult>
    {
        public IEnumerable<ElementToImport> Elements { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
    }
}
