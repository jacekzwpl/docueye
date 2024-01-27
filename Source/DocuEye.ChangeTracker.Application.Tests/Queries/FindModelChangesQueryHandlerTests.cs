using DocuEye.ChangeTracker.Application.Queries.FindModelChanges;

namespace DocuEye.ChangeTracker.Application.Tests.Queries
{
    public class FindModelChangesQueryHandlerTests : BaseChangeTrackerTests
    {
        [Test]
        public async Task WhenFindModelChangesQueryAppropriateNumberOfItemsAreReturned()
        {
            // Arrange
            var query = new FindModelChangesQuery()
            {
                WorkspaceId = "workspacetest1"
            };

            // Act
            var handler = new FindModelChangesQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.GreaterThan(0), "Hanlder should return one or more items.");
        }
    }
}
