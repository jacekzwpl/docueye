


namespace DocuEye.DocsKeeper.Application.Tests
{
    public abstract class BaseDocsKeeperTests
    {
        protected FakeDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            this.dbContext = new FakeDbContext();
        }
    }
}
