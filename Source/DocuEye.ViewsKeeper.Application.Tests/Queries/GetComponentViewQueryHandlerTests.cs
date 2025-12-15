using DocuEye.ViewsKeeper.Application.Queries.GetComponentView;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class GetComponentViewQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetComponentViewQueryItemIsReturned()
        {
            // Arrange
            var query = new GetComponentViewQuery("ComponentViewId1", "workspacetest1");

            // Act
            var handler = new GetComponentViewQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

    }
}
