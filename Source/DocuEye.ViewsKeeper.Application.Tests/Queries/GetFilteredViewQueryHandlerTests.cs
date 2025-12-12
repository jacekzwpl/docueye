using DocuEye.ViewsKeeper.Application.Queries.GetFilteredView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class GetFilteredViewQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetFilteredViewQueryItemIsReturned()
        {
            // Arrange
            var query = new GetFilteredViewQuery("FilteredViewId1", "workspacetest1");

            // Act
            var handler = new GetFilteredViewQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

    }
}
