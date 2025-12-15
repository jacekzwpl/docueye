using DocuEye.ModelKeeper.Application.Queries.GetChildElements;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Application.Tests;

namespace DocuEye.ModelKeeper.Application.Tests.Queries
{
    public class GetChildElementsQueryHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetChildElementsQueryThenAllChildElementsAreReturned()
        {
            // Arrange
            var query = new GetChildElementsQuery("workspacetest1", "SoftwareSystemId1");

            // Act
            var handler = new GetChildElementsQueryHandler(this.dbContext);
            var items = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(2), "Hanlder should return 2 items.");
        }

        [Test]
        public async Task WhenHanldeGetChildElementsQueryWithTypeFilterThenChildElementsWithTypeAreReturned()
        {
            // Arrange
            var query = new GetChildElementsQuery("workspacetest1", "DeploymentNodeId1", ElementType.DeploymentNode);

            // Act
            var handler = new GetChildElementsQueryHandler(this.dbContext);
            var items = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(2), "Hanlder should return 2 items.");
        }
    }
}
