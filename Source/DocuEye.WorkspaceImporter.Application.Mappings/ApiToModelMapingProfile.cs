using AutoMapper;
using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.WorkspacesKeeper.Model;

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
            CreateMap<ViewToImport, FilteredView>()
                .ForMember(dest => dest.Elements, opt => opt.Ignore())
                .ForMember(dest => dest.Relationships, opt => opt.Ignore())
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.FilteredView));

            CreateMap<DocumentationSectionToImport, DocumentationSection>();
            CreateMap<DocumentationToImport, Documentation>();
            CreateMap<ImageToImport, Image>();

            CreateMap<DecisionToImport, Decision>()
                .ForMember(dest => dest.DslId, opt => opt.MapFrom(src => src.StrucuturizrId))
                .ForMember(dest => dest.Links, opt => opt.Ignore());
            CreateMap<DecisionLinkToImport, DecisionLink>();

            CreateMap<ViewConfigurationToImport, ViewConfiguration>();
            CreateMap<ElementStyleToImport, ElementStyle>();
            CreateMap<RelationshipStyleToImport, RelationshipStyle>();
            CreateMap<TerminologyToImport, Terminology>();
        }
    }
}
