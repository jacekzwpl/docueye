namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// System Context view
    /// </summary>
    public class SystemContextView : StaticDiagramView
    {
        /// <summary>
        /// The ID of the software system this view is associated with.
        /// </summary>
        public string? SoftwareSystemId { get; set; }
        /// <summary>
        /// Specifies whether the enterprise boundary (to differentiate internal elements from external elements) should be visible on the resulting diagram.
        /// </summary>
        public bool? EnterpriseBoundaryVisible { get; set; }
    }
}
