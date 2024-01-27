namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Container View
    /// </summary>
    public class ContainerView : StaticDiagramView
    {
        /// <summary>
        /// The ID of the software system this view is associated with.
        /// </summary>
        public string? SoftwareSystemId { get; set; }
        /// <summary>
        /// Specifies whether software system boundaries should be visible for \"external\" containers (those outside the software system in scope).
        /// </summary>
        public bool? ExternalSoftwareSystemBoundariesVisible { get; set; }
    }
}
