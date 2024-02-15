using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships;
using DocuEye.ViewsKeeper.Application.Tests;

namespace DocuEye.ModelKeeper.Application.Tests.Queries
{
    public class GetAllWorkspaceRelationshipsQueryHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenHandleGetAllWorkspaceRelationshipsQueryThenAllWorspacRelationshipsAreReturned()
        {
            // Arrange
            var query = new GetAllWorkspaceRelationshipsQuery("workspacetest1");

            // Act
            var handler = new GetAllWorkspaceRelationshipsQueryHandler(this.dbContext);
            var items = await handler.Handle(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(4), "Hanlder should return 4 items.");
        }
    }
}
