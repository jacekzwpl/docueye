namespace DocuEye.WorkspaceImporter.Application.Commands.ImportViews
{
    public class ImportViewsResult : ImportResult
    {
        public ImportViewsResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public ImportViewsResult(string workspaceId, bool isSuccess, string? message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
