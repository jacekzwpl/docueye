using DocuEye.ViewsKeeper.Application.Queries.GetDynamicView;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class GetDynamicViewQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetDynamicViewQueryItemIsReturned()
        {
            // Arrange
            var query = new GetDynamicViewQuery("DynamicViewId1", "workspacetest1");

            // Act
            var handler = new GetDynamicViewQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

    }
}
