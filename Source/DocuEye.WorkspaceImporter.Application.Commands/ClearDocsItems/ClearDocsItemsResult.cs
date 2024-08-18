namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDocsItems
{
    public class ClearDocsItemsResult : ImportResult
    {
        public ClearDocsItemsResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public ClearDocsItemsResult(string workspaceId, bool isSuccess, string message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
