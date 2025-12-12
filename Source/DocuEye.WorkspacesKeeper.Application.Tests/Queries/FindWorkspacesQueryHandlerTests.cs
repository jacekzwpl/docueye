using DocuEye.WorkspacesKeeper.Application.Queries.FindWorspaces;

namespace DocuEye.WorkspacesKeeper.Application.Tests.Queries
{
    public class FindWorkspacesQueryHandlerTests : BaseWorkspacesKeeperTests
    {
        [Test]
        public async Task WhenHandleFindWorkspacesWithNoFilterThenAllWorkspacesAreReturned()
        {
            // Arrange
            var query = new FindWorkspacesQuery();

            // Act
            var handler = new FindWorkspacesQueryHandler(this.dbContext);
            var items = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(3), "Hanlder should return 3 items.");
        }


        [Test]
        public async Task WhenHandleFindWorkspacesWithNameFilterThenAllWorkspacesMachingCriteriaAreReturned()
        {
            // Arrange
            var query = new FindWorkspacesQuery() { 
                Name = "My Worspace"
            };

            // Act
            var handler = new FindWorkspacesQueryHandler(this.dbContext);
            var items = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(1), "Hanlder should return 1 item.");
        }

        [Test]
        public async Task WhenHandleFindWorkspacesWithLimitThenOnlyLimitedNumberOfWorkspacesAreReturned()
        {
            // Arrange
            var query = new FindWorkspacesQuery()
            {
                Limit = 2,
                Skip = 0
            };

            // Act
            var handler = new FindWorkspacesQueryHandler(this.dbContext);
            var items = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(2), "Hanlder should return 2 items.");

        }

    }
}
