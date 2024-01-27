using DocuEye.ViewsKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Dynamic view exploded from structurizr json
    /// </summary>
    public class ExplodedDynamicView
    {
        /// <summary>
        /// The ID of context element in structurizr json
        /// </summary>
        public string? StructurizrElementId { get; set; }
        /// <summary>
        /// Dynamic view data
        /// </summary>
        public DynamicView View { get; set; } = new DynamicView();
        /// <summary>
        /// List of exploded elements from structurizr json that are present in view
        /// </summary>
        public List<ExplodedElementView> Elements { get; set; } = new List<ExplodedElementView>();
        /// <summary>
        /// List of exploded relationships from structurizr json that are present in view
        /// </summary>
        public List<ExplodedRelationshipView> Relationships { get; set; } = new List<ExplodedRelationshipView>();
    }
}
