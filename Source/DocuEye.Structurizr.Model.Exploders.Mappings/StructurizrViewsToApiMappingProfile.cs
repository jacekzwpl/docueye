using AutoMapper;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.Structurizr.Model.Exploders.Mappings
{
    public class StructurizrViewsToApiMappingProfile : Profile
    {
        public StructurizrViewsToApiMappingProfile()
        {
            CreateMap<StructurizrRelationshipView, RelationshipInView>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id));

            CreateMap<StructurizrElementView, ElementInView>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.X))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Y));

            CreateMap<StructurizrAutomaticLayout, ViewAutomaticLayout>()
                .ForMember(dest => dest.Implementation, opt => opt.MapFrom(src => src.Implementation))
                .ForMember(dest => dest.RankDirection, opt => opt.MapFrom(src => src.RankDirection))
                .ForMember(dest => dest.RankSeparation, opt => opt.MapFrom(src => src.RankSeparation))
                .ForMember(dest => dest.NodeSeparation, opt => opt.MapFrom(src => src.NodeSeparation))
                .ForMember(dest => dest.EdgeSeparation, opt => opt.MapFrom(src => src.EdgeSeparation))
                .ForMember(dest => dest.Vertices, opt => opt.MapFrom(src => src.Vertices));

            CreateMap<StructurizrSystemLandscapeView, ViewToImport>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.SystemLandscapeView))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForMember(dest => dest.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.ExternalBoundariesVisible, opt => opt.MapFrom(src => src.EnterpriseBoundaryVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrSystemContextView, ViewToImport>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.SystemContextView))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.SoftwareSystemId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForMember(dest => dest.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.ExternalBoundariesVisible, opt => opt.MapFrom(src => src.EnterpriseBoundaryVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrContainerView, ViewToImport>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.ContainerView))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.SoftwareSystemId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForMember(dest => dest.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.ExternalBoundariesVisible, opt => opt.MapFrom(src => src.ExternalSoftwareSystemBoundariesVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrComponentView, ViewToImport>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.ComponentView))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.ContainerId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForMember(dest => dest.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.ExternalBoundariesVisible, opt => opt.MapFrom(src => src.ExternalContainerBoundariesVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrDynamicView, ViewToImport>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.DynamicView))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.ElementId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForMember(dest => dest.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.ExternalBoundariesVisible, opt => opt.MapFrom(src => src.ExternalBoundariesVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrDeploymentView, ViewToImport>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.DeploymentView))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.SoftwareSystemId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForMember(dest => dest.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrFilteredView, ViewToImport>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.FilteredView))
                .ForMember(dest => dest.BaseViewKey, opt => opt.MapFrom(src => src.BaseViewKey))
                .ForMember(dest => dest.Mode, opt => opt.MapFrom(src => src.Mode))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key));

            CreateMap<StructurizrImageView, ViewToImport>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.ImageView))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.ElementId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key));

        }
    }
}
