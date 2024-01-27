namespace DocuEye.ModelKeeper.Application.Queries.GetChildElements
{
    /// <summary>
    /// Represents child element of another element
    /// </summary>
    public class ChildElement
    {
        /// <summary>
        /// Id of child element
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Name of child element
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Type of child element
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// Description of child element
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The ID of element that this element is instance of
        /// </summary>
        public string? InstanceOfId { get; set; }
    }
}
