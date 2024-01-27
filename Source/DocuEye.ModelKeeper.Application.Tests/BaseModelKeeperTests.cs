using AutoMapper;
using DocuEye.ModelKeeper.Application.Mappings;

namespace DocuEye.ViewsKeeper.Application.Tests
{
    public abstract class BaseModelKeeperTests
    {
        protected FakeDbContext dbContext;
        protected IMapper mapper;

        [SetUp]
        public void Setup()
        {
            this.dbContext = new FakeDbContext();
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<ModelKeeperMappingProfile>());
            mapper = config.CreateMapper();
        }
    }
}
