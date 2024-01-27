using AutoMapper;
using DocuEye.WorkspacesKeeper.Application.Queries.FindWorspaces;
using DocuEye.WorkspacesKeeper.Model;

namespace DocuEye.WorkspacesKeeper.Application.Mappings
{
    /// <summary>
    /// Automapper profile for WorkspaceKeeper module
    /// </summary>
    public class WorkspacesKeeperMappingProfile : Profile
    {
        /// <summary>
        /// Creates instance
        /// </summary>
        public WorkspacesKeeperMappingProfile() {

            CreateMap<Workspace, FoundedWorkspace>();
        }
    }
}
