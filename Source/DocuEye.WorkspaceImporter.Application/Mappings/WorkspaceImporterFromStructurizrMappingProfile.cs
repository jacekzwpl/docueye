using AutoMapper;
using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.ViewsExploder;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspaceImporter.Application.Mappings
{
    /// <summary>
    /// Automapper profile for mappings from Structurizr items 
    /// </summary>
    public class WorkspaceImporterFromStructurizrMappingProfile : Profile
    {
        /// <summary>
        /// Creates instance
        /// </summary>
        public WorkspaceImporterFromStructurizrMappingProfile() {

            CreateMap<StructurizrDocumentationSection, DocumentationSection>();
            CreateMap<StructurizrImage, Image>();

            CreateMap<StructurizrDecision, ExplodedDecision>()
                .ForMember(dest => dest.DslId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.ElementId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.ElementId, opt => opt.Ignore())
                .ForMember(dest => dest.StructurizrLinks, opt => opt.MapFrom(src => src.Links))
                .ForMember(dest => dest.Links, opt => opt.Ignore());

            CreateMap<StructurizrDecisionLink, ExplodedDecisionLink>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<StructurizrSoftwareSystem, ExplodedElement>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.SoftwareSystem))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")))
                .ForMember(dest => dest.DocumentationSections, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Sections))
                .ForMember(dest => dest.DocumentationImages, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Images))
                .ForMember(dest => dest.DocumentationDecisions, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Decisions));
            CreateMap<StructurizrContainer, ExplodedElement>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Container))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")))
                .ForMember(dest => dest.DocumentationSections, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Sections))
                .ForMember(dest => dest.DocumentationImages, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Images))
                .ForMember(dest => dest.DocumentationDecisions, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Decisions));
            CreateMap<StructurizrComponent, ExplodedElement>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Component))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")))
                .ForMember(dest => dest.DocumentationSections, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Sections))
                .ForMember(dest => dest.DocumentationImages, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Images))
                .ForMember(dest => dest.DocumentationDecisions, opt => opt.MapFrom(src => src.Documentation == null ? null : src.Documentation.Decisions));
            CreateMap<StructurizrPerson, ExplodedElement>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Person))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));
            CreateMap<StructurizrDeploymentNode, ExplodedElement>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.DeploymentNode))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));
            CreateMap<StructurizrContainerInstance, ExplodedElement>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.ContainerInstance))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")))
                .ForMember(dest => dest.StructurizrInstanceOfId, opt => opt.MapFrom(src => src.ContainerId));
            CreateMap<StructurizrSoftwareSystemInstance, ExplodedElement>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.SoftwareSystemInstance))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")))
                .ForMember(dest => dest.StructurizrInstanceOfId, opt => opt.MapFrom(src => src.SoftwareSystemId));

            CreateMap<StructurizrInfrastructureNode, ExplodedElement>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.InfrastructureNode))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrRelationship, ExplodedRelationship>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SourceId, opt => opt.Ignore())
                .ForMember(dest => dest.DestinationId, opt => opt.Ignore())
                .ForMember(dest => dest.LinkedRelationshipId, opt => opt.Ignore())
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StructurizrSourceId, opt => opt.MapFrom(src => src.SourceId))
                .ForMember(dest => dest.StructurizrDestinationId, opt => opt.MapFrom(src => src.DestinationId))
                .ForMember(dest => dest.StructurizrLinkedRelationshipId, opt => opt.MapFrom(src => src.LinkedRelationshipId))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrAutomaticLayout, AutomaticLayout>();

            CreateMap<StructurizrElementView, ExplodedElementView>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id));

            CreateMap<StructurizrRelationshipView, ExplodedRelationshipView>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id));

            CreateMap<StructurizrSystemLandscapeView, ExplodedSystemLandscapeView>()
                .ForPath(dest => dest.View.ViewType, opt => opt.MapFrom(src => ViewType.SystemLandscapeView))
                .ForPath(dest => dest.View.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.View.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.View.Key, opt => opt.MapFrom(src => src.Key))
                .ForPath(dest => dest.View.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForPath(dest => dest.View.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForPath(dest => dest.View.EnterpriseBoundaryVisible, opt => opt.MapFrom(src => src.EnterpriseBoundaryVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrSystemContextView, ExplodedSystemContextView>()
                .ForPath(dest => dest.View.ViewType, opt => opt.MapFrom(src => ViewType.SystemContextView))
                .ForPath(dest => dest.View.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.View.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.View.Key, opt => opt.MapFrom(src => src.Key))
                .ForPath(dest => dest.View.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForPath(dest => dest.View.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.StructurizrSoftwareSystemId, opt => opt.MapFrom(src => src.SoftwareSystemId))
                .ForPath(dest => dest.View.EnterpriseBoundaryVisible, opt => opt.MapFrom(src => src.EnterpriseBoundaryVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrContainerView, ExplodedContainerView>()
                .ForPath(dest => dest.View.ViewType, opt => opt.MapFrom(src => ViewType.ContainerView))
                .ForPath(dest => dest.View.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.View.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.View.Key, opt => opt.MapFrom(src => src.Key))
                .ForPath(dest => dest.View.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForPath(dest => dest.View.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.StructurizrSoftwareSystemId, opt => opt.MapFrom(src => src.SoftwareSystemId))
                .ForPath(dest => dest.View.ExternalSoftwareSystemBoundariesVisible, opt => opt.MapFrom(src => src.ExternalSoftwareSystemBoundariesVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrComponentView, ExplodedComponentView>()
                .ForPath(dest => dest.View.ViewType, opt => opt.MapFrom(src => ViewType.ComponentView))
                .ForPath(dest => dest.View.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.View.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.View.Key, opt => opt.MapFrom(src => src.Key))
                .ForPath(dest => dest.View.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForPath(dest => dest.View.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.StructurizrContainerId, opt => opt.MapFrom(src => src.ContainerId))
                .ForPath(dest => dest.View.ExternalContainerBoundariesVisible, opt => opt.MapFrom(src => src.ExternalContainerBoundariesVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));
            
            CreateMap<StructurizrDynamicView, ExplodedDynamicView>()
                .ForPath(dest => dest.View.ViewType, opt => opt.MapFrom(src => ViewType.DynamicView))
                .ForPath(dest => dest.View.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.View.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.View.Key, opt => opt.MapFrom(src => src.Key))
                .ForPath(dest => dest.View.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForPath(dest => dest.View.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.ElementId))
                .ForPath(dest => dest.View.ExternalBoundariesVisible, opt => opt.MapFrom(src => src.ExternalBoundariesVisible))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrDeploymentView, ExplodedDeploymentView>()
                .ForPath(dest => dest.View.ViewType, opt => opt.MapFrom(src => ViewType.DeploymentView))
                .ForPath(dest => dest.View.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.View.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.View.Key, opt => opt.MapFrom(src => src.Key))
                .ForPath(dest => dest.View.PaperSize, opt => opt.MapFrom(src => src.PaperSize))
                .ForPath(dest => dest.View.AutomaticLayout, opt => opt.MapFrom(src => src.AutomaticLayout))
                .ForMember(dest => dest.StructuruzrSoftwareSystemId, opt => opt.MapFrom(src => src.SoftwareSystemId))
                .ForMember(dest => dest.Elements, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

            CreateMap<StructurizrImageView, ExplodedImageView>()
                .ForPath(dest => dest.View.ViewType, opt => opt.MapFrom(src => ViewType.ImageView))
                .ForPath(dest => dest.View.Title, opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.View.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.View.Key, opt => opt.MapFrom(src => src.Key))
                .ForPath(dest => dest.View.Content, opt => opt.MapFrom(src => src.Content))
                .ForPath(dest => dest.View.ContentType, opt => opt.MapFrom(src => src.ContentType))
                .ForMember(dest => dest.StructurizrElementId, opt => opt.MapFrom(src => src.ElementId));
            
            CreateMap<StructurizrFilteredView, FilteredView>()
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => ViewType.FilteredView));



            CreateMap<StructurizrConfigurationStyles, ViewConfiguration>()
                .ForMember(dest => dest.ElementStyles, opt => opt.MapFrom(src => src.Elements))
                .ForMember(dest => dest.RelationshipStyles, opt => opt.MapFrom(src => src.Relationships));
            CreateMap<StructurizrElementStyle, ElementStyle>();
            CreateMap<StructurizrRelationshipStyle, RelationshipStyle>();

            CreateMap<StructurizrTerminology, Terminology>();

            CreateMap<StructurizrWorkspaceConfigurationUser, WorkspaceAccessRule>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

        }

        private static List<string>? SplitStringToList(string? str, string separator)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str.Split(",").ToList();
        }

        

        
    }
}
