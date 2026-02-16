using DocuEye.DocsKeeper.Application.Queries.GetDocumentationFile;

namespace DocuEye.DocsKeeper.Application.Tests.Queries
{
    public class GetDocumentationFileQueryTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenGetDocumentationFileThenFileDataIsReturned()
        {
            // Arrange
            var query = new GetDocumentationFileQuery
            {
                WorkspaceId = "workspacetest1",
                ElementId = "elementtestId1",
                DocumentationType = "openapi"
            };

            // Act
            var handler = new GetDocumentationFileQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

        [Test]
        public async Task WhenGetNonExistingDocumentationFileThenNullIsReturned()
        {
            // Arrange
            var query = new GetDocumentationFileQuery
            {
                WorkspaceId = "workspacetest1",
                ElementId = "elementtestId1-notexisting",
                DocumentationType = "openapi"
            };

            // Act
            var handler = new GetDocumentationFileQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, Is.Null, "Hanlder should return null.");
        }
    }
}
