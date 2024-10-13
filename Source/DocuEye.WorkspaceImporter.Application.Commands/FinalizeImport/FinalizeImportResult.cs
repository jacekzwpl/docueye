namespace DocuEye.WorkspaceImporter.Application.Commands.FinalizeImport
{
    public class FinalizeImportResult : ImportResult
    {
        public FinalizeImportResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public FinalizeImportResult(string workspaceId, bool isSuccess, string? message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
