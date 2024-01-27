using DocuEye.Infrastructure.MongoDB;
using DocuEye.WorkspacesKeeper.Model;

namespace DocuEye.WorkspacesKeeper.Persistence
{
    /// <summary>
    /// MongoDB context for WorkspacesKeeper module
    /// </summary>
    public interface IWorkspacesKeeperDBContext
    {
        /// <summary>
        /// Collection of workspaces
        /// </summary>
        IGenericCollection<Workspace> Workspaces { get; }
        /// <summary>
        /// Collection of view configurations
        /// </summary>
        IGenericCollection<ViewConfiguration> ViewConfigurations { get; }
    }
}
