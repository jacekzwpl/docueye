

namespace DocuEye.ViewsKeeper.Application.Tests
{
    public abstract class BaseViewsKeeperTests
    {
        protected FakeDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            this.dbContext = new FakeDbContext();
        }
    }
}
