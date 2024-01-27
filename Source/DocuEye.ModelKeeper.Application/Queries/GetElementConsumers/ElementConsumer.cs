namespace DocuEye.ModelKeeper.Application.Queries.GetElementConsumers
{
    /// <summary>
    /// Represents consumer of element
    /// </summary>
    public class ElementConsumer
    {
        /// <summary>
        /// Consumer element ID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// The Name of the consumer element
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Type of the consumer element
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// The description of the consumer element
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The relationship description between consumer and the element
        /// </summary>
        public string? RelationDescription { get; set; }
        /// <summary>
        /// The relationship technology between consumer and the element
        /// </summary>
        public string? RelationTechnology { get; set; }
    }
}
