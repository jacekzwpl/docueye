using DocuEye.ViewsKeeper.Application.Queries.GetImageView;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class GetImageViewQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetImageViewQueryItemIsReturned()
        {
            // Arrange
            var query = new GetImageViewQuery("ImageViewId1", "workspacetest1");

            // Act
            var handler = new GetImageViewQueryHandler(this.dbContext);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

    }
}
