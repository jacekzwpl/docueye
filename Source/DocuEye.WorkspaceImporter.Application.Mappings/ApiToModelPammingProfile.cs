using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System;

namespace DocuEye.WorkspaceImporter.Application.Mappings
{
    public class ApiToModelPammingProfile : Profile
    {
        public ApiToModelPammingProfile()
        {
            CreateMap<ElementToImport, Element>();
            CreateMap<RelationshipToImport, Relationship>();
        }
    }
}
