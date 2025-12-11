using DocuEye.ModelKeeper.Application.Queries.GetWorspaceCatalogElements;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Application.Tests;

namespace DocuEye.ModelKeeper.Application.Tests.Queries
{
    public class GetWorspaceCatalogElementsQueryHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetWorspaceCatalogElementsQueryThenAllElementsWithNoInstancesAreReturned()
        {
            // Arrange
            var query = new GetWorspaceCatalogElementsQuery("workspacetest1");

            // Act
            var handler = new GetWorspaceCatalogElementsQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(9), "Hanlder should return 9 items.");
            var instacesCount = items.Where(o => o.Type == ElementType.SoftwareSystemInstance || o.Type == ElementType.ContainerInstance).Count();
            Assert.That(instacesCount, Is.EqualTo(0), "Hanlder should return 0 instances.");
        }


        [Test]
        public async Task WhenHanldeGetWorspaceCatalogElementsQueryWithNameFilterThenAllMatchingElementsAreReturned()
        {
            // Arrange
            var query = new GetWorspaceCatalogElementsQuery("workspacetest1", "My System");

            // Act
            var handler = new GetWorspaceCatalogElementsQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(2), "Hanlder should return 2 items.");
            var instacesCount = items.Where(o => o.Type == ElementType.SoftwareSystemInstance || o.Type == ElementType.ContainerInstance).Count();
            Assert.That(instacesCount, Is.EqualTo(0), "Hanlder should return 0 instances.");
        }

        [Test]
        public async Task WhenHanldeGetWorspaceCatalogElementsQueryWithLimitFilterThenAllNoMoreThenLimitElementsAreReturned()
        {
            // Arrange
            var query = new GetWorspaceCatalogElementsQuery("workspacetest1", null, null, 2, 0);

            // Act
            var handler = new GetWorspaceCatalogElementsQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(2), "Hanlder should return 2 items.");
          
        }

        [Test]
        public async Task WhenHanldeGetWorspaceCatalogElementsQueryWithTypeFilterThenOnlyElementsWithSelectedTypeAreReturned()
        {
            // Arrange
            var query = new GetWorspaceCatalogElementsQuery("workspacetest1", null, ElementType.Container);

            // Act
            var handler = new GetWorspaceCatalogElementsQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Where(o => o.Type != ElementType.Container).Count, Is.EqualTo(0), "Hanlder should return only items with type Container.");

        }


    }
}
