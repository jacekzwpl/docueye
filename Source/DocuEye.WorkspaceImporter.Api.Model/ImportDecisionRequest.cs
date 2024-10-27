using DocuEye.WorkspaceImporter.Api.Model.Docs;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportDecisionRequest
    {
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
        public DecisionToImport Decision { get; set; } = null!;
    }
}
