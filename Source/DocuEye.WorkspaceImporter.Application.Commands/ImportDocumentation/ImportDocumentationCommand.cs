using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.Docs;

using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation
{
    public class ImportDocumentationCommand : ICommand<ImportDocumentationResult>
    {
        public string WorkspaceId { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public DocumentationToImport Documentation { get; set; } = null!;

        public ImportDocumentationCommand(string workspaceId, string importKey, DocumentationToImport documentation)
        {
            WorkspaceId = workspaceId;
            ImportKey = importKey;
            Documentation = documentation;
        }

    }
}
