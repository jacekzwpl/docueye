using DocuEye.ModelKeeper.Application.EventHandlers;
using DocuEye.ViewsKeeper.Application.Tests;
using DocuEye.WorkspacesKeeper.Application.Events;

namespace DocuEye.ModelKeeper.Application.Tests.EventHandlers
{
    public class WorkspaceDeletedEventHandlerTests : BaseModelKeeperTests
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
            var elements = await this.dbContext.Elements.Find(o => o.WorkspaceId == "workspacetest1");
            var relationships = await this.dbContext.Relationships.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(elements.Count, Is.EqualTo(0), "There should be no element with WorkspaceId = workspacetest1  in database");
            Assert.That(relationships.Count, Is.EqualTo(0), "There should be no relationship with WorkspaceId = workspacetest1  in database");

        }
    }
}
