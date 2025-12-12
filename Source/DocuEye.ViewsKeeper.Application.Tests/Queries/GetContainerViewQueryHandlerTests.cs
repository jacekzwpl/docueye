using DocuEye.ViewsKeeper.Application.Queries.GetContainerView;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class GetContainerViewQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetContainerViewQueryItemIsReturned()
        {
            // Arrange
            var query = new GetContainerViewQuery("ContainerViewId1", "workspacetest1");

            // Act
            var handler = new GetContainerViewQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

    }
}
