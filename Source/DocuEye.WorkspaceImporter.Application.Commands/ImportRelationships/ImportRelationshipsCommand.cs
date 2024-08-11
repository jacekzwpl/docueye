using DocuEye.WorkspaceImporter.Api.Model;
using MediatR;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportRelationships
{
    public class ImportRelationshipsCommand : IRequest<ImportRelationshipsResult>
    {
        public IEnumerable<RelationshipToImport> Relationships { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
    }
}
