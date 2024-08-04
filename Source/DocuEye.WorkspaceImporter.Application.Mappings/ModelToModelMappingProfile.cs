using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.WorkspaceImporter.Application.Mappings
{
    public class ModelToModelMappingProfile : Profile
    {
        public ModelToModelMappingProfile()
        {
            CreateMap<Element, ElementView>();
            CreateMap<Relationship, RelationshipView>();

        }
    }
}
