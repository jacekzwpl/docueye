using AutoMapper;
using DocuEye.ViewsKeeper.Application.Queries.FindViewsWithElement;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.ViewsKeeper.Application.Mappings
{
    /// <summary>
    /// Automapper profile for ViewsKeeper module
    /// </summary>
    public class ViewsKeeperMappingProfile : Profile
    {
        /// <summary>
        /// Creates instance
        /// </summary>
        public ViewsKeeperMappingProfile() {


            CreateMap<SystemLandscapeView, ViewWithElement>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<SystemContextView, ViewWithElement>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<ContainerView, ViewWithElement>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<ComponentView, ViewWithElement>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<DeploymentView, ViewWithElement>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<DynamicView, ViewWithElement>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => SetViewName(src.ViewType, src.Key, src.Title, src.Description)));
            CreateMap<ImageView, ViewWithElement>()
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
