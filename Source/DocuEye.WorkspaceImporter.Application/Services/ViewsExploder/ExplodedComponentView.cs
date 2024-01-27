using DocuEye.ViewsKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Component view exploded from structurizr json
    /// </summary>
    public class ExplodedComponentView
    {
        /// <summary>
        /// The ID of context container in structurizr json
        /// </summary>
        public string? StructurizrContainerId { get; set; }
        /// <summary>
        /// Component view data
        /// </summary>
        public ComponentView View { get; set; } = new ComponentView();
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
