using DocuEye.ViewsKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Result of exploding views from structurizr json
    /// </summary>
    public class ViewsExplodeResult
    {
        /// <summary>
        /// List of exploded System landscape views
        /// </summary>
        public List<ExplodedSystemLandscapeView> LandscapeViews { get; set; } = new List<ExplodedSystemLandscapeView>();
        /// <summary>
        /// List of exploded System Context views
        /// </summary>
        public List<ExplodedSystemContextView> SystemContextViews { get; set; } = new List<ExplodedSystemContextView>();
        /// <summary>
        /// List of exploded Container views
        /// </summary>
        public List<ExplodedContainerView> ContainerViews { get; set; } = new List<ExplodedContainerView>();
        /// <summary>
        /// List of exploded Component views
        /// </summary>
        public List<ExplodedComponentView> ComponentViews { get; set; } = new List<ExplodedComponentView>();
        /// <summary>
        /// List of exploded Dynamic views
        /// </summary>
        public List<ExplodedDynamicView> DynamicViews { get; set; } = new List<ExplodedDynamicView>();
        /// <summary>
        /// List of exploded Deployment views
        /// </summary>
        public List<ExplodedDeploymentView> DeploymentViews { get; set; } = new List<ExplodedDeploymentView>();
        /// <summary>
        /// List of exploded images views
        /// </summary>
        public List<ExplodedImageView> ImagesViews { get; set; } = new List<ExplodedImageView>();
        /// <summary>
        /// List of filtered views exploded from structurizr dsl
        /// </summary>
        public List<FilteredView> FilteredViews { get; set; } = new List<FilteredView>();

    }
}
