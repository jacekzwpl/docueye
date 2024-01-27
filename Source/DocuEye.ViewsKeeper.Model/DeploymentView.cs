namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Deployment View
    /// </summary>
    public class DeploymentView : StaticDiagramView
    {
        /// <summary>
        /// The ID of the software system this view is associated with.
        /// </summary>
        public string? SoftwareSystemId { get; set; }
    }
}
