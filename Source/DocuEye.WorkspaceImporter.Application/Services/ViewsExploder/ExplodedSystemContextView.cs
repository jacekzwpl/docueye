using DocuEye.ViewsKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// System context view exploded from structurizr json
    /// </summary>
    public class ExplodedSystemContextView
    {
        /// <summary>
        /// The ID of context software system in structurizr json
        /// </summary>
        public string? StructurizrSoftwareSystemId { get; set; }
        /// <summary>
        /// System context view data
        /// </summary>
        public SystemContextView View { get; set; } = new SystemContextView();
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
