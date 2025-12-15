using System;
using System.Collections.Generic;
using System.Text;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportRelationshipsRequest
    {
        public IEnumerable<RelationshipToImport> Relationships { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
    }
}
