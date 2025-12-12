using DocuEye.ModelKeeper.Application.Queries.GetElement;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Application.Tests;

namespace DocuEye.ModelKeeper.Application.Tests.Queries
{
    public class GetElementQueryHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetElementQueryThenElementIseReturned()
        {
            // Arrange
            var query = new GetElementQuery("ContainerId2", "workspacetest1");

            // Act
            var handler = new GetElementQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
            Assert.That(item.Type, Is.EqualTo(ElementType.Container), "Hanlder should return item fo type Container");
        }
    }
}
