namespace DocuEye.WorkspacesKeeper.Model
{
    /// <summary>
    /// Workspace View
    /// </summary>
    public class WorkspaceView
    {
        /// <summary>
        /// The ID of view
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// View name
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Views type
        /// </summary>
        public string ViewType { get; set; } = null!;
    }
}
