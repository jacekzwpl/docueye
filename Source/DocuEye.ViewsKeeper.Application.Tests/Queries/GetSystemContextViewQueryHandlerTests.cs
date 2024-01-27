using DocuEye.ViewsKeeper.Application.Queries.GetSystemContextView;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class GetSystemContextViewQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetSystemContextViewQueryItemIsReturned()
        {
            // Arrange
            var query = new GetSystemContextViewQuery("SystemContextViewId1", "workspacetest1");

            // Act
            var handler = new GetSystemContextViewQueryHandler(this.dbContext);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

    }
}
