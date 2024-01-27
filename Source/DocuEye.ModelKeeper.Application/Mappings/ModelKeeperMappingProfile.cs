using AutoMapper;
using DocuEye.ModelKeeper.Application.Queries.GetChildElements;
using DocuEye.ModelKeeper.Application.Queries.GetElementConsumers;
using DocuEye.ModelKeeper.Application.Queries.GetElementDependences;
using DocuEye.ModelKeeper.Application.Queries.GetWorspaceCatalogElements;
using DocuEye.ModelKeeper.Model;

namespace DocuEye.ModelKeeper.Application.Mappings
{
    /// <summary>
    /// Automapper profile for modelKeeper module
    /// </summary>
    public class ModelKeeperMappingProfile : Profile
    {
        public ModelKeeperMappingProfile()
        {
            CreateMap<Element, WorkspaceCatalogElement>();
            CreateMap<Element, ChildElement>();
            CreateMap<Element, ElementDependence>();
            CreateMap<Element, ElementConsumer>();

        }
    }
}
