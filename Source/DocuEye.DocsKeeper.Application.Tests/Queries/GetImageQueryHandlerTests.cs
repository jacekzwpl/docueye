using DocuEye.DocsKeeper.Application.Queries.GetImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Tests.Queries
{
    public class GetImageQueryHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetImageQueryImageIsReturned()
        {
            // Arrange
            var query = new GetImageQuery("documentationId1", "workspacetest1","images/name.png");

            // Act
            var handler = new GetImageQueryHandler(this.dbContext);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }
    }
}
