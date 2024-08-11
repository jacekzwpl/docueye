using DocuEye.WorkspaceImporter.Api.Model.Views;
using MediatR;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportViews
{
    public class ImportViewsCommand : IRequest<ImportViewsResult>
    {
        public IEnumerable<ViewToImport> Views { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
    }
}
