using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.ViewsKeeper.Model;

using System.Collections.Generic;
using System.Linq;


namespace DocuEye.ViewsKeeper.Application.Commands.SaveViewsChanges
{
    /// <summary>
    /// Save view changes command
    /// </summary>
    public class SaveViewsChangesCommand : ICommand
    {
        /// <summary>
        /// The ID of workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// System Landscape Views that should exists in workspace after save
        /// </summary>
        public IEnumerable<SystemLandscapeView> SystemLandscapeViews { get; set; } = Enumerable.Empty<SystemLandscapeView>();
        /// <summary>
        /// System Context Views that should exists in workspace after save
        /// </summary>
        public IEnumerable<SystemContextView> SystemContextViews { get; set; } = Enumerable.Empty<SystemContextView>();
        /// <summary>
        /// Container Views that should exists in workspace after save
        /// </summary>
        public IEnumerable<ContainerView> ContainerViews { get; set; } = Enumerable.Empty<ContainerView>();
        /// <summary>
        /// Component Views that should exists in workspace after save
        /// </summary>
        public IEnumerable<ComponentView> ComponentViews { get; set; } = Enumerable.Empty<ComponentView>();
        /// <summary>
        /// Dynamic Views that should exists in workspace after save
        /// </summary>
        public IEnumerable<DynamicView> DynamicViews { get; set; } = Enumerable.Empty<DynamicView>();
        /// <summary>
        /// Deployment Views that should exists in workspace after save
        /// </summary>
        public IEnumerable<DeploymentView> DeploymentViews { get; set; } = Enumerable.Empty<DeploymentView>();
        /// <summary>
        /// Filtered Views that should exists in workspace after save
        /// </summary>
        public IEnumerable<FilteredView> FilteredViews { get; set; } = Enumerable.Empty<FilteredView>();
        /// <summary>
        /// Image Views that should exists in workspace after save
        /// </summary>
        public IEnumerable<ImageView> ImagesViews { get; set; } = Enumerable.Empty<ImageView>();
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
