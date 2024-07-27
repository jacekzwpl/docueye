using AutoMapper;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model.Exploders.Mappings
{
    public class StructurizrDocumentationToApiModelMappingProfile : Profile
    {
        public StructurizrDocumentationToApiModelMappingProfile() {

            CreateMap<StructurizrDocumentationSection, DocumentationSectionToImport>();

            CreateMap<StructurizrImage, ImageToImport>()
                .ForMember(dest => dest.DocumentationId, opt => opt.Ignore());

            CreateMap<StructurizrDecisionLink, DecisionLinkToImport>()
                .ForMember(dest => dest.StructurizrId, opt => opt.MapFrom(src => src.Id));

            CreateMap<StructurizrDecision, DecisionToImport>()
                .ForMember(dest => dest.DocumentationId, opt => opt.Ignore())
                .ForMember(dest => dest.StrucuturizrId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StrucuturizrElementId, opt => opt.MapFrom(src => src.ElementId));

        }
    }
}
