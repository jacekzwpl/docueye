using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Model.Exploders.Mappings
{
    public class StructurizrModelToApiMappingProfile : Profile
    {
        public StructurizrModelToApiMappingProfile()
        {
            CreateMap<StructurizrJsonPerson, ElementToImport>()
               .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Person))
               .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrJsonComponent, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Component))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrJsonContainer, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Container))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrJsonSoftwareSystem, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.SoftwareSystem))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));
            
            CreateMap<StructurizrJsonDeploymentNode, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.DeploymentNode))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrJsonSoftwareSystemInstance, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.SoftwareSystemInstance))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")))
                .ForMember(dest => dest.StructurizrInstanceOfId, opt => opt.MapFrom(src => src.SoftwareSystemId));

            CreateMap<StructurizrJsonContainerInstance, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.ContainerInstance))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")))
                .ForMember(dest => dest.StructurizrInstanceOfId, opt => opt.MapFrom(src => src.ContainerId));

            CreateMap<StructurizrJsonInfrastructureNode, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.InfrastructureNode))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrJsonRelationship, RelationshipToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StructurizrSourceId, opt => opt.MapFrom(src => src.SourceId))
                .ForMember(dest => dest.StructurizrDestinationId, opt => opt.MapFrom(src => src.DestinationId))
                .ForMember(dest => dest.StructurizrLinkedRelationshipId, opt => opt.MapFrom(src => src.LinkedRelationshipId))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));
        }

        private static List<string>? SplitStringToList(string? str, string separator)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str.Split(",").ToList();
        }
    }
}
