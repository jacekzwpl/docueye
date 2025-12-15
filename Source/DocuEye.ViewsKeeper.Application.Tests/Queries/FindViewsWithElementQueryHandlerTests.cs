using DocuEye.ViewsKeeper.Application.Queries.FindViewsWithElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class FindViewsWithElementQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeFindViewsWithElementQueryItemIsReturned()
        {
            // Arrange
            var query = new FindViewsWithElementQuery("elementId1", "workspacetest1");

            // Act
            var handler = new FindViewsWithElementQueryHandler(this.dbContext);
            var items = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(items.Count, Is.GreaterThan(0), "Hanlder should return more than zero items.");
        }

    }
}
