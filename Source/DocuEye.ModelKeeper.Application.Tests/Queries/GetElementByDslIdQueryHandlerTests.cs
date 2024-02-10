using DocuEye.ModelKeeper.Application.Queries.GetElementByDslId;
using DocuEye.ViewsKeeper.Application.Tests;

namespace DocuEye.ModelKeeper.Application.Tests.Queries
{
    public class GetElementByDslIdQueryHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenGetElementByDslIdThenElementIsReturned()
        {
            // Arrange
            var query = new GetElementByDslIdQuery("DSL-SoftwareSystemId1", "workspacetest1");

            // Act
            var handler = new GetElementByDslIdQueryHandler(this.dbContext);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

        [Test]
        public async Task WhenGetElementByDslIdTahnDoesNotExistsThenNullIsReturned()
        {
            // Arrange
            var query = new GetElementByDslIdQuery("nonexisting", "workspacetest1");

            // Act
            var handler = new GetElementByDslIdQueryHandler(this.dbContext);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, Is.Null, "Hanlder should return null.");
            
        }
    }
}
