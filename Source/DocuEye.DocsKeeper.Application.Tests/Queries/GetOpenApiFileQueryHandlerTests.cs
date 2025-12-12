using DocuEye.DocsKeeper.Application.Queries.GetOpenApiFile;

namespace DocuEye.DocsKeeper.Application.Tests.Queries
{
    public class GetOpenApiFileQueryHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenGetOpenApiFileThenFileDataIsReturned()
        {
            // Arrange
            var query = new GetOpenApiFileQuery("workspacetest1", "elementtestId1");

            // Act
            var handler = new GetOpenApiFileQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

        [Test]
        public async Task WhenGetNonExistingOpenApiFileThenNullIsReturned()
        {
            // Arrange
            var query = new GetOpenApiFileQuery("workspacetest1", "elementtestId1-notexisting");

            // Act
            var handler = new GetOpenApiFileQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, Is.Null, "Hanlder should return null.");
        }
    }
}
