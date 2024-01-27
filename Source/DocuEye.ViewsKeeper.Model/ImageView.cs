namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Image View
    /// </summary>
    public class ImageView : BaseView
    {
        /// <summary>
        /// The ID of the element this view is associated with (optional).
        /// </summary>
        public string? ElementId { get; set; }
        /// <summary>
        /// Image content
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// Image content type
        /// </summary>
        public string? ContentType { get; set; }
    }
}
