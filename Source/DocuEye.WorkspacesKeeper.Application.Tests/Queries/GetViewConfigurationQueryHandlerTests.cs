using DocuEye.WorkspacesKeeper.Application.Queries.GetViewConfiguration;

namespace DocuEye.WorkspacesKeeper.Application.Tests.Queries
{
    public class GetViewConfigurationQueryHandlerTests : BaseWorkspacesKeeperTests
    {
        [Test]
        public async Task WhenHandleViewConfigurationQueryThenViewConfigurationIsReturned()
        {
            // Arrange
            var query = new GetViewConfigurationQuery("workspacetest1");

            // Act
            var handler = new GetViewConfigurationQueryHandler(this.dbContext);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return ViewConfiguration.");
        }

        [Test]
        public async Task WhenWorkspaceDoNotHaveViewConfigurationThenNullIsReturned()
        {
            // Arrange
            var query = new GetViewConfigurationQuery("workspacetest2");

            // Act
            var handler = new GetViewConfigurationQueryHandler(this.dbContext);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, Is.Null, "Hanlder should return null.");
        }
    }
}
