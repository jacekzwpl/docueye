using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ViewsKeeper.Application.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Tests.Queries
{
    public class GetAllWorkspaceElementsQueryHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenHandleGetAllWorkspaceElementsQueryThenAllWorspaceelementsAreReturned()
        {
            // Arrange
            var query = new GetAllWorkspaceElementsQuery("workspacetest1");

            // Act
            var handler = new GetAllWorkspaceElementsQueryHandler(this.dbContext);
            var items = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(items.Count, Is.EqualTo(10), "Hanlder should return 10 items.");
        }
    }
}
