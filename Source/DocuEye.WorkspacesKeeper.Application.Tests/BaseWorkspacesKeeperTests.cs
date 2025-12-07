

namespace DocuEye.WorkspacesKeeper.Application.Tests
{
    public abstract class BaseWorkspacesKeeperTests
    {
        protected FakeDbContext dbContext;


        [SetUp]
        public void Setup()
        {
            this.dbContext = new FakeDbContext();
        }
    }
}
