using DocuEye.ViewsKeeper.Model;
using MediatR;
using System.Collections.Generic;

namespace DocuEye.ViewsKeeper.Application.Commands.SaveViewsChanges
{
    /// <summary>
    /// Save view changes command
    /// </summary>
    public class SaveViewsChangesCommand : IRequest
    {
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// System Landscape Views that should exists in workspace after save
        /// </summary>
        public List<SystemLandscapeView> SystemLandscapeViews { get; set; } = new List<SystemLandscapeView>();
        /// <summary>
        /// System Context Views that should exists in workspace after save
        /// </summary>
        public List<SystemContextView> SystemContextViews { get; set; } = new List<SystemContextView>();
        /// <summary>
        /// Container Views that should exists in workspace after save
        /// </summary>
        public List<ContainerView> ContainerViews { get; set; } = new List<ContainerView>();
        /// <summary>
        /// Component Views that should exists in workspace after save
        /// </summary>
        public List<ComponentView> ComponentViews { get; set; } = new List<ComponentView>();
        /// <summary>
        /// Dynamic Views that should exists in workspace after save
        /// </summary>
        public List<DynamicView> DynamicViews { get; set; } = new List<DynamicView>();
        /// <summary>
        /// Deployment Views that should exists in workspace after save
        /// </summary>
        public List<DeploymentView> DeploymentViews { get; set; } = new List<DeploymentView>();
        /// <summary>
        /// Filtered Views that should exists in workspace after save
        /// </summary>
        public List<FilteredView> FilteredViews { get; set; } = new List<FilteredView>();
        /// <summary>
        /// Image Views that should exists in workspace after save
        /// </summary>
        public List<ImageView> ImagesViews { get; set; } = new List<ImageView>();
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        public SaveViewsChangesCommand(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }
    }
}
