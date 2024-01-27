using AutoMapper;
using DocuEye.DocsKeeper.Application.Mappings;

namespace DocuEye.DocsKeeper.Application.Tests
{
    public abstract class BaseDocsKeeperTests
    {
        protected FakeDbContext dbContext;
        protected IMapper mapper;

        [SetUp]
        public void Setup()
        {
            this.dbContext = new FakeDbContext();
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<DocsKeeperMappingProfile>());
            mapper = config.CreateMapper();
        }
    }
}
