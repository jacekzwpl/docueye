using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.WorkspaceImporter.Application.Mappings
{
    public class ApiToModelMapingProfile : Profile
    {
        public ApiToModelMapingProfile()
        {
            CreateMap<ElementToImport, Element>();
            CreateMap<RelationshipToImport, Relationship>();
            

            CreateMap<ViewAutomaticLayout, AutomaticLayout>();
            CreateMap<ViewToImport, SystemLandscapeView>()
                .ForMember(dest => dest.Elements, opt => opt.Ignore())
                .ForMember(dest => dest.Relationships, opt => opt.Ignore())
                .ForMember(dest => dest.EnterpriseBoundaryVisible, opt => opt.MapFrom(src => src.ExternalBoundariesVisible))
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.SystemLandscapeView));
            CreateMap<ViewToImport, SystemContextView>()
                .ForMember(dest => dest.Elements, opt => opt.Ignore())
                .ForMember(dest => dest.Relationships, opt => opt.Ignore())
                .ForMember(dest => dest.EnterpriseBoundaryVisible, opt => opt.MapFrom(src => src.ExternalBoundariesVisible))
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.SystemContextView));
            CreateMap<ViewToImport, ContainerView>()
                .ForMember(dest => dest.Elements, opt => opt.Ignore())
                .ForMember(dest => dest.Relationships, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalSoftwareSystemBoundariesVisible, opt => opt.MapFrom(src => src.ExternalBoundariesVisible))
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.ContainerView));
            CreateMap<ViewToImport, ComponentView>()
                .ForMember(dest => dest.Elements, opt => opt.Ignore())
                .ForMember(dest => dest.Relationships, opt => opt.Ignore())
                .ForMember(dest => dest.ExternalContainerBoundariesVisible, opt => opt.MapFrom(src => src.ExternalBoundariesVisible))
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.ComponentView));
            CreateMap<ViewToImport, DeploymentView>()
                .ForMember(dest => dest.Elements, opt => opt.Ignore())
                .ForMember(dest => dest.Relationships, opt => opt.Ignore())
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.DeploymentView));
            CreateMap<ViewToImport, ImageView>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.ImageView));
            CreateMap<ViewToImport, DynamicView>()
                .ForMember(dest => dest.Elements, opt => opt.Ignore())
                .ForMember(dest => dest.Relationships, opt => opt.Ignore())
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.DynamicView));
        }
    }
}
