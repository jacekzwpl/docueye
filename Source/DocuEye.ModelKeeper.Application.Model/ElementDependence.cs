namespace DocuEye.ModelKeeper.Application.Model
{
    /// <summary>
    /// Represents the dependence of the element
    /// </summary>
    public class ElementDependence
    {
        /// <summary>
        /// ID of dependence element 
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// ID name of dependence element
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// The type of dependence element
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// The description of dependence element
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Relationship description between element and its dependency
        /// </summary>
        public string? RelationDescription { get; set; }
        /// <summary>
        /// Relationship technology between element and its dependency
        /// </summary>
        public string? RelationTechnology { get; set; }
    }
}
