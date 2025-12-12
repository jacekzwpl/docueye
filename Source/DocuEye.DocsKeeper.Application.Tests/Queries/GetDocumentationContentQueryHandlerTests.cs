using DocuEye.DocsKeeper.Application.Queries.GetWorkspaceDocumentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Tests.Queries
{
    public class GetDocumentationContentQueryHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetDocumentationContentQueryForWorkspaceContentWasReturned()
        {
            // Arrange
            var query = new GetDocumentationContentQuery("workspacetest1", "https://docueye.com");

            // Act
            var handler = new GetDocumentationContentQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item?.HtmlContent, !Is.Null, "Hanlder should return documentation with content.");
        }

        [Test]
        public async Task WhenHanldeGetDocumentationContentQueryForElementContentWasReturned()
        {
            // Arrange
            var query = new GetDocumentationContentQuery("workspacetest1", "https://docueye.com", "elementId1");

            // Act
            var handler = new GetDocumentationContentQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item?.HtmlContent, !Is.Null, "Hanlder should return documentation with content.");
        }
    }
}
