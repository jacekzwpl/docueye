using DocuEye.WorkspacesKeeper.Application.Queries.GetWorkspace;

namespace DocuEye.WorkspacesKeeper.Application.Tests.Queries
{
    public class GetWorkspaceQueryHandlerTests : BaseWorkspacesKeeperTests
    {
        [Test]
        public async Task WhenHandleGetWorkspaceQueryThenWorkspaceIsReturned()
        {
            // Arrange
            var query = new GetWorkspaceQuery("workspacetest1");

            // Act
            var handler = new GetWorkspaceQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return Worskpace.");
        }

        [Test]
        public async Task WhenThereIsNoWorkspaceWithGivenIDThenNullIsReturned()
        {
            // Arrange
            var query = new GetWorkspaceQuery("nonexisitngworspace");

            // Act
            var handler = new GetWorkspaceQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, Is.Null, "Hanlder should return null.");
        }
    }
}
