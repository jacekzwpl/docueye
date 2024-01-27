namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// System Landscape View
    /// </summary>
    public class SystemLandscapeView : StaticDiagramView
    {
        /// <summary>
        /// Specifies whether the enterprise boundary (to differentiate internal elements from external elements) should be visible on the resulting diagram.
        /// </summary>
        public bool? EnterpriseBoundaryVisible { get; set; }
    }
}
