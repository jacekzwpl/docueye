namespace DocuEye.ModelKeeper.Application.Model
{
    /// <summary>
    /// Represents element in workspace elements catalog
    /// </summary>
    public class WorkspaceCatalogElement
    {
        /// <summary>
        /// ID o the element
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// The name of the element
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Type of the element
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// The description of the element
        /// </summary>
        public string? Description { get; set; }
    }
}
