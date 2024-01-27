using DocuEye.DocsKeeper.Application.Queries.GetDecisionContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Tests.Queries
{
    public class GetDecisionQueryHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetWorkspaceDecisionQueryIsReturned()
        {
            // Arrange
            var query = new GetDecisionQuery("workspacetest1", "decisionId1","https://docueye.com");

            // Act
            var handler = new GetDecisionQueryHandler(this.dbContext);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }
    }
}
