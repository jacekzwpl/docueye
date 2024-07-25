using AutoMapper;
using DocuEye.Structurizr.Model.Exploders.Mappings;

namespace DocuEye.Structurizr.Model.Exploders.Tests
{
    public class BaseExploderTests
    {
        protected IMapper mapper;

        [SetUp]
        public void Setup()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.AddProfile<StructurizrModelToApiMappingProfile>();
                cfg.AddProfile<StructurizrViewsToApiMappingProfile>();
            });
            mapper = config.CreateMapper();
        }
    }
}
