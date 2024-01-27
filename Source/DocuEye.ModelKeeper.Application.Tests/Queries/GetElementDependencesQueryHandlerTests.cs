using DocuEye.ModelKeeper.Application.Queries.GetElementDependences;
using DocuEye.ViewsKeeper.Application.Tests;

namespace DocuEye.ModelKeeper.Application.Tests.Queries
{
    public class GetElementDependencesQueryHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetElementDependencesQueryThenAllDependencesAreReturned()
        {
            // Arrange
            var query = new GetElementDependencesQuery("ContainerId2", "workspacetest1");

            // Act
            var handler = new GetElementDependencesQueryHandler(this.dbContext, this.mapper);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(1), "Hanlder should return 1 item.");
        }

        [Test]
        public async Task WhenHanldeGetElementDependencesQueryForElementWithNoDependencesThenNoDependecesAreReturned()
        {
            // Arrange
            var query = new GetElementDependencesQuery("ContainerId4", "workspacetest1");

            // Act
            var handler = new GetElementDependencesQueryHandler(this.dbContext, this.mapper);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(0), "Hanlder should return 0 items.");
        }
    }
}
