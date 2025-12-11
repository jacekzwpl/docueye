using DocuEye.ViewsKeeper.Application.Queries.GetSystemLandscapeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Tests.Queries
{
    public class GetSystemLandscapeViewQueryHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHanldeGetSystemLandscapeViewQueryItemIsReturned()
        {
            // Arrange
            var query = new GetSystemLandscapeViewQuery("SystemLandscapeViewId1", "workspacetest1");

            // Act
            var handler = new GetSystemLandscapeViewQueryHandler(this.dbContext);
            var item = await handler.HandleAsync(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return item.");
        }

    }
}
