using DocuEye.ViewsKeeper.Model;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Relationship in view exploded from structurizr json
    /// </summary>
    public class ExplodedRelationshipView : RelationshipView
    {
        /// <summary>
        /// The ID of relationship in structurizr json
        /// </summary>
        public string StructurizrId { get; set; } = null!;
    }
}
