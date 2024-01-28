using AutoMapper;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspacesKeeper.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector
{
    /// <summary>
    /// Result of detecting changes in views
    /// </summary>
    public class ViewsChangesResult
    {
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mapper">Automapper service</param>
        public ViewsChangesResult(IMapper mapper)
        {
            this.mapper = mapper;
        }
        /// <summary>
        /// System Landscape Views that should be created
        /// </summary>
        public List<SystemLandscapeView> SystemLandscapeViewsToAdd { get; set; } = new List<SystemLandscapeView>();
        /// <summary>
        /// System Context Views that should be created
        /// </summary>
        public List<SystemContextView> SystemContextViewsToAdd { get; set; } = new List<SystemContextView>();
        /// <summary>
        /// Container Views that should be created
        /// </summary>
        public List<ContainerView> ContainerViewsToAdd { get; set; } = new List<ContainerView>();
        /// <summary>
        /// Component Views that should be created
        /// </summary>
        public List<ComponentView> ComponentViewsToAdd { get; set; } = new List<ComponentView>();
        /// <summary>
        /// Dynamic Views that should be created
        /// </summary>
        public List<DynamicView> DynamicViewsToAdd { get; set; } = new List<DynamicView>();
        /// <summary>
        /// Deployment Views that should be created
        /// </summary>
        public List<DeploymentView> DeploymentViewsToAdd { get; set; } = new List<DeploymentView>();
        /// <summary>
        /// Filtered Views that should be created
        /// </summary>
        public List<FilteredView> FilteredViewsToAdd { get; set; } = new List<FilteredView>();
        /// <summary>
        /// Images Views that should be created
        /// </summary>
        public List<ImageView> ImagesViewsToAdd { get; set; } = new List<ImageView>();
        /// <summary>
        /// Simplified views for saving with workspace
        /// </summary>
        public List<WorkspaceView> WorkspaceViews { 
            get {
                var workspaceViews = new List<WorkspaceView>();
                workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(this.SystemLandscapeViewsToAdd));
                workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(this.SystemContextViewsToAdd));
                workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(this.ContainerViewsToAdd));
                workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(this.ComponentViewsToAdd));
                workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(this.DeploymentViewsToAdd));
                workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(this.DynamicViewsToAdd));
                workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(this.ImagesViewsToAdd));
                workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(this.FilteredViewsToAdd));
                return workspaceViews;
            } 
        }
        /// <summary>
        /// Gets elements from view of given key
        /// </summary>
        /// <param name="viewKey">View key</param>
        /// <returns>List of view elements</returns>
        public IEnumerable<ElementView> GetElementsByViewKey(string viewKey)
        {
            StaticDiagramView? view = this.SystemLandscapeViewsToAdd.FirstOrDefault(o => o.Key == viewKey);
            if(view != null)
            {
                return view.Elements;
            }

            view = this.SystemContextViewsToAdd.FirstOrDefault(o => o.Key == viewKey);
            if (view != null)
            {
                return view.Elements;
            }

            view = this.ContainerViewsToAdd.FirstOrDefault(o => o.Key == viewKey);
            if (view != null)
            {
                return view.Elements;
            }

            view = this.ComponentViewsToAdd.FirstOrDefault(o => o.Key == viewKey);
            if (view != null)
            {
                return view.Elements;
            }

            return Enumerable.Empty<ElementView>();
        }
        /// <summary>
        /// Gets elationships from view of given key
        /// </summary>
        /// <param name="viewKey">view key</param>
        /// <returns>List of view relationships</returns>
        public IEnumerable<RelationshipView> GetRelationshipsByViewKey(string viewKey)
        {
            StaticDiagramView? view = this.SystemLandscapeViewsToAdd.FirstOrDefault(o => o.Key == viewKey);
            if (view != null)
            {
                return view.Relationships;
            }

            view = this.SystemContextViewsToAdd.FirstOrDefault(o => o.Key == viewKey);
            if (view != null)
            {
                return view.Relationships;
            }

            view = this.ContainerViewsToAdd.FirstOrDefault(o => o.Key == viewKey);
            if (view != null)
            {
                return view.Relationships;
            }

            view = this.ComponentViewsToAdd.FirstOrDefault(o => o.Key == viewKey);
            if (view != null)
            {
                return view.Relationships;
            }

            return Enumerable.Empty<RelationshipView>();
        }

    }
}
