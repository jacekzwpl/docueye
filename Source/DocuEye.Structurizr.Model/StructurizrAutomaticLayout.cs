namespace DocuEye.Structurizr.Model
{
    public class StructurizrAutomaticLayout
    {
        /// <summary>
        /// The automatic layout implementation.
        /// </summary>
        public string? Implementation {  get; set; }
        /// <summary>
        /// The algorithm rank direction.
        /// </summary>
        public string? RankDirection { get; set; }
        /// <summary>
        /// The separation between ranks (pixels).
        /// </summary>
        public int? RankSeparation { get; set; }
        /// <summary>
        /// The separation between nodes in the same rank (pixels).
        /// </summary>
        public int? NodeSeparation { get; set; }
        /// <summary>
        /// The separation between edges (pixels).
        /// </summary>
        public int? EdgeSeparation { get; set; }
        /// <summary>
        /// Whether vertices should be created during automatic layout.
        /// </summary>
        public bool? Vertices { get; set; } 
    }
}
