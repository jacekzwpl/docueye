using DocuEye.DocsKeeper.Model;

namespace DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder
{
    /// <summary>
    /// Decision link exploded from structurizr json
    /// </summary>
    public class ExplodedDecisionLink : DecisionLink
    {
        /// <summary>
        /// ID of linked decision in structurizr json
        /// </summary>
        public string? StructurizrId { get; set; }
    }
}
