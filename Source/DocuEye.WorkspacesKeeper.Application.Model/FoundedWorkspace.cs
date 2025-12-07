namespace DocuEye.WorkspacesKeeper.Application.Model
{
    /// <summary>
    /// Represents workspace that was found in query
    /// </summary>
    public class FoundedWorkspace
    {
        /// <summary>
        /// the Id of workspace
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// The name of the workspace
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Description of the workspace
        /// </summary>
        public string? Description { get; set; }

    }
}
