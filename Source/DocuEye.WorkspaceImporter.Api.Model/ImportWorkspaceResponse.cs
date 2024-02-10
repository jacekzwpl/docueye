namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportWorkspaceResponse
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Indicates if import was finished with success
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Import result message
        /// </summary>
        public string? Message { get; set; }
    }
}
