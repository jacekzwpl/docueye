using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Commands.ImportWorkspace;
using DocuEye.WorkspacesKeeper.Model;

namespace DocuEye.WorkspaceImporter.Application.Mappings
{
    /// <summary>
    /// Automapper profile for WorkspaceImporter module
    /// </summary>
    public class WorkspaceImporterMappingProfile : Profile
    {
        /// <summary>
        /// Creates instance
        /// </summary>
        public WorkspaceImporterMappingProfile() {


            CreateMap<SystemLandscapeView, WorkspaceView>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<SystemContextView, WorkspaceView>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<ContainerView, WorkspaceView>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<ComponentView, WorkspaceView>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<DeploymentView, WorkspaceView>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<DynamicView, WorkspaceView>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<ImageView, WorkspaceView>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<FilteredView, WorkspaceView>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));


        }

        private static string SetViewName(string type,string? key, string? title, string? description)
        {
            if(!string.IsNullOrWhiteSpace(title))
            {
                return string.Format("[{0}]{1}", type, title);

            }
            else if (!string.IsNullOrWhiteSpace(description))
            {
                return string.Format("[{0}]{1}", type, description);
            }
            else 
            {
                return string.Format("[{0}]{1}", type, key);
            }
            
        }

        
    }
}
