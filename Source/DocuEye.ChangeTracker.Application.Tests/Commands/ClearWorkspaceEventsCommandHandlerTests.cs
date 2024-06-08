using DocuEye.ChangeTracker.Application.Commands.ClearWorkspaceEvents;

namespace DocuEye.ChangeTracker.Application.Tests.Commands
{
    public class ClearWorkspaceEventsCommandHandlerTests : BaseChangeTrackerTests
    {
        [Test]
        public async Task WhenClearWorkspaceEventsThenNoDataForWorkspaceIsPresent()
        {
            // Arrange
            var command = new ClearWorkspaceEventsCommand("workspacetest1");

            // Act
            var handler = new ClearWorkspaceEventsCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var modelChanges = await this.dbContext.ModelChanges.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(modelChanges.Count, Is.EqualTo(0), "No model changes should be present.");
        }
    }
}
