using DocuEye.ChangeTracker.Application.EventHandlers;
using DocuEye.WorkspacesKeeper.Application.Events;

namespace DocuEye.ChangeTracker.Application.Tests.EventHandlers
{
    public class WorkspaceDeletedEventHandlerTests : BaseChangeTrackerTests
    {
        [Test]
        public async Task WhenWorkspaceDeletedThenNoDataForWorkspaceIsPresent()
        {
            // Arrange
            var notification = new WorkspaceDeletedEvent("workspacetest1");

            // Act
            var handler = new WorkspaceDeletedEventHandler(this.dbContext);
            await handler.Handle(notification, default);

            // Assert
            var modelChanges = await this.dbContext.ModelChanges.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(modelChanges.Count, Is.EqualTo(0), "No model changes should be present.");

        }

    }
}
