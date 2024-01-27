namespace DocuEye.ViewsKeeper.Application.Queries.FindViewsWithElement
{
    /// <summary>
    /// Represents view that contains element
    /// </summary>
    public class ViewWithElement
    {
        /// <summary>
        /// The ID of view
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// The name of view
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// The type of view
        /// </summary>
        public string ViewType { get; set; } = null!;
    }
}
