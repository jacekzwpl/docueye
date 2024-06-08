using DocuEye.WorkspacesKeeper.Application.EventHandlers;
using DocuEye.WorkspacesKeeper.Application.Events;

namespace DocuEye.WorkspacesKeeper.Application.Tests.EventHandlers
{
    public class WorkspaceDeletedEventHandlerTests : BaseWorkspacesKeeperTests
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
            var viewConfigurations = await this.dbContext.ViewConfigurations.Find(o => o.Id == "workspacetest1");
            Assert.That(viewConfigurations.Count, Is.EqualTo(0), "No ViewConfigurations should exists.");
            var workspace = await this.dbContext.Workspaces.FindOne(o => o.Id == "workspacetest1");
            Assert.That(workspace, Is.Null, "Workspace should not exists.");

        }
    }
}
