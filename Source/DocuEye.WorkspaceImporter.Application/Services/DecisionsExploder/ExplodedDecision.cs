using DocuEye.DocsKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder
{
    /// <summary>
    /// Decision that was exploded form structurizr
    /// </summary>
    public class ExplodedDecision : Decision
    {
        /// <summary>
        /// The ID of decision in structurizr json
        /// </summary>
        public string? StructurizrElementId { get; set; }
        /// <summary>
        /// Decision links exploded from structurizr json
        /// </summary>
        public IEnumerable<ExplodedDecisionLink>? StructurizrLinks { get; set; }
    }
}
