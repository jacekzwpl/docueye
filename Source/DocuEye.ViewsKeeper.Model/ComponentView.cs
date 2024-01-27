namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Component View
    /// </summary>
    public class ComponentView : StaticDiagramView
    {
        /// <summary>
        /// The ID of the container this view is associated with.
        /// </summary>
        public string? ContainerId { get; set; }
        /// <summary>
        /// Specifies whether container boundaries should be visible for \"external\" components (those outside the container in scope).
        /// </summary>
        public bool? ExternalContainerBoundariesVisible { get; set; }
    }
}
