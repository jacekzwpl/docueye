using DocuEye.Infrastructure.MongoDB;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.ViewsKeeper.Persistence
{
    /// <summary>
    /// MongoDB context for ViewsKeeper module
    /// </summary>
    public interface IViewsKeeperDBContext
    {
        /// <summary>
        /// Collection of all views
        /// </summary>
        IGenericCollection<BaseView> AllViews { get; }
        /// <summary>
        /// Collection of system landscape views
        /// </summary>
        IGenericCollection<SystemLandscapeView> SystemLandscapeViews { get; }
        /// <summary>
        /// Collection of system context views
        /// </summary>
        IGenericCollection<SystemContextView> SystemContextViews { get; }
        /// <summary>
        /// Collection of container views
        /// </summary>
        IGenericCollection<ContainerView> ContainerViews { get; }
        /// <summary>
        /// Collection of component views
        /// </summary>
        IGenericCollection<ComponentView> ComponentViews { get; }
        /// <summary>
        /// Collection of dynamic views
        /// </summary>
        IGenericCollection<DynamicView> DynamicViews { get; }
        /// <summary>
        /// Collection of deployment views
        /// </summary>
        IGenericCollection<DeploymentView> DeploymentViews { get; }
        /// <summary>
        /// Collection of filtered views
        /// </summary>
        IGenericCollection<FilteredView> FilteredViews { get; }
        /// <summary>
        /// Collection fo image views
        /// </summary>
        IGenericCollection<ImageView> ImageViews { get; }
    }
}
