namespace DocuEye.DocsKeeper.Model
{
    /// <summary>
    /// Link between decisions
    /// </summary>
    public class DecisionLink
    {
        /// <summary>
        /// Linked decision ID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// The title of the decisions link
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// The description of the decisions link
        /// </summary>
        public string? Description { get; set; }
    }
}
