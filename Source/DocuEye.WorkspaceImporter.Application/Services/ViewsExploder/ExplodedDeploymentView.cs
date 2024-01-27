using DocuEye.ViewsKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Deployment view exploded from structurizr json
    /// </summary>
    public class ExplodedDeploymentView
    {
        /// <summary>
        /// The ID of context software system in structurizr json
        /// </summary>
        public string? StructuruzrSoftwareSystemId { get; set; }
        /// <summary>
        /// Deployment view data
        /// </summary>
        public DeploymentView View { get; set; } = new DeploymentView();
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
