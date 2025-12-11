using DocuEye.WorkspacesKeeper.Application.Events;
using DocuEye.ViewsKeeper.Application.EventHanlders;

namespace DocuEye.ViewsKeeper.Application.Tests.EventHandlers
{
    public class WorkspaceDeletedEventHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenWorkspaceDeletedThenNoDataForWorkspaceIsPresent()
        {
            // Arrange
            var notification = new WorkspaceDeletedEvent("workspacetest1");

            // Act
            var handler = new WorkspaceDeletedEventHandler(this.dbContext);
            await handler.HandleAsync(notification, default);

            // Assert
            var items = await this.dbContext.AllViews.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(items.Count, Is.EqualTo(0), "There should be no views in workspace");

        }
    }
}
