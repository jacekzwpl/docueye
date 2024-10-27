using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportDecisionsLinksRequest
    {
        public string WorkspaceId { get; set; } = null!;

        public string ImportKey { get; set; } = null!;

        public string DocumentationId { get; set; } = null!;
        public string DecisionDslId { get; set; } = null!;
        public IEnumerable<DecisionLinkToImport> DecisionsLinks { get; set; } = null!;
    }
}
