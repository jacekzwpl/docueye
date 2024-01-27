using AutoMapper;
using DocuEye.ViewsKeeper.Application.Mappings;

namespace DocuEye.ViewsKeeper.Application.Tests
{
    public abstract class BaseViewsKeeperTests
    {
        protected FakeDbContext dbContext;
        protected IMapper mapper;

        [SetUp]
        public void Setup()
        {
            this.dbContext = new FakeDbContext();
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<ViewsKeeperMappingProfile>());
            mapper = config.CreateMapper();
        }
    }
}
