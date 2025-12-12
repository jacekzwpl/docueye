using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;

using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportRelationships
{
    public class ImportRelationshipsCommand : ICommand<ImportRelationshipsResult>
    {
        public IEnumerable<RelationshipToImport> Relationships { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
    }
}
