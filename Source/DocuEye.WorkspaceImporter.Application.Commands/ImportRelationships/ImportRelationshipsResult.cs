namespace DocuEye.WorkspaceImporter.Application.Commands.ImportRelationships
{
    public class ImportRelationshipsResult : ImportResult
    {
        public ImportRelationshipsResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public ImportRelationshipsResult(string workspaceId, bool isSuccess, string? message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
