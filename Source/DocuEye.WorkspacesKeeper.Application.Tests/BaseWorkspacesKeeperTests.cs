using AutoMapper;
using DocuEye.WorkspacesKeeper.Application.Mappings;

namespace DocuEye.WorkspacesKeeper.Application.Tests
{
    public abstract class BaseWorkspacesKeeperTests
    {
        protected FakeDbContext dbContext;
        protected IMapper mapper;

        [SetUp]
        public void Setup()
        {
            this.dbContext = new FakeDbContext();
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<WorkspacesKeeperMappingProfile>());
            mapper = config.CreateMapper();
        }
    }
}
