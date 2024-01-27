using DocuEye.ModelKeeper.Model;

namespace DocuEye.WorkspaceImporter.Application.Services.ModelExploder
{
    /// <summary>
    /// Relationship exploded from structurizr json
    /// </summary>
    public class ExplodedRelationship : Relationship
    {
        /// <summary>
        /// The ID of relationship in structurizr json
        /// </summary>
        public string StructurizrId { get; set; } = null!;
        /// <summary>
        /// The ID of source element for this relationship in structurizr json
        /// </summary>
        public string StructurizrSourceId { get; set; } = null!;
        /// <summary>
        /// The ID of destination element for this realtionship in structurizr json
        /// </summary>
        public string StructurizrDestinationId { get; set; } = null!;
        /// <summary>
        /// The ID of linked relationship in structuriz json
        /// </summary>
        public string? StructurizrLinkedRelationshipId { get; set; }

    }
}
