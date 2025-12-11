using DocuEye.ModelKeeper.Application.Queries.GetElementConsumers;
using DocuEye.ViewsKeeper.Application.Tests;

namespace DocuEye.ModelKeeper.Application.Tests.Queries
{
    public class GetElementConsumersQueryHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenGetElementConsumersThenAllConsumersWithOutLinkedAreReturned()
        {
            // Arrange
            var query = new GetElementConsumersQuery("ContainerId1", "workspacetest1");

            // Act
            var handler = new GetElementConsumersQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(2), "Hanlder should return 2 items.");
        }

        [Test]
        public async Task WhenGetElementConsumersWithLinkedThenAllConsumersAreReturned()
        {
            // Arrange
            var query = new GetElementConsumersQuery("ContainerId1", "workspacetest1", true);

            // Act
            var handler = new GetElementConsumersQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(3), "Hanlder should return 3 items.");
        }

        [Test]
        public async Task WhenHanldeGetElementConsumersQueryForElementWithNoConsumersThenNoConsumersAreReturned()
        {
            // Arrange
            var query = new GetElementConsumersQuery("ContainerId2", "workspacetest1");

            // Act
            var handler = new GetElementConsumersQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(0), "Hanlder should return 0 items.");
        }
    }
}
