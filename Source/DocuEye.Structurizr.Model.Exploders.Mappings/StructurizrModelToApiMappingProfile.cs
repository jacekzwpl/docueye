using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Model.Exploders.Mappings
{
    public class StructurizrModelToApiMappingProfile : Profile
    {
        public StructurizrModelToApiMappingProfile()
        {
            CreateMap<StructurizrPerson, ElementToImport>()
               .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Person))
               .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrComponent, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Component))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrContainer, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.Container))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));

            CreateMap<StructurizrSoftwareSystem, ElementToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => ElementType.SoftwareSystem))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => SplitStringToList(src.Tags, ",")));
        }

        private static List<string>? SplitStringToList(string? str, string separator)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str.Split(",").ToList();
        }
    }
}
