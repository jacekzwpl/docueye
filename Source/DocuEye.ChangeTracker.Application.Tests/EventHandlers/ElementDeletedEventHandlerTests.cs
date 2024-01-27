using DocuEye.ChangeTracker.Application.EventHandlers;
using DocuEye.WorkspaceImporter.Application.Events;

namespace DocuEye.ChangeTracker.Application.Tests.EventHandlers
{
    public class ElementDeletedEventHandlerTests : BaseChangeTrackerTests
    {
        [Test]
        public async Task WhenHandleElementDeletedEventModelChangeIsPresentInDatabase()
        {
            // Arrange
            var notification = new ElementDeletedEvent(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                null,
                "elementname"
                );

            // Act
            var handler = new ElementDeletedEventHandler(this.dbContext);
            await handler.Handle(notification, default);

            // Assert
            var modelChange = await this.dbContext.ModelChanges.FindOne(o => o.ElementId == notification.ElementId);
            Assert.That(modelChange, !Is.Null, "Model change event shuold be present in database.");
        }

        [Test]
        public async Task WhenHandleElementDeletedEventForIdenticalImportIdOnlyOneModelChangeIsPresentInDataBase()
        {
            // Arrange
            var notification = new ElementDeletedEvent(
                "workspacetest1",
                "elementtest1",
                "importtest1",
                "imporkey",
                null,
                "elementname"
            );

            // Act
            var handler = new ElementDeletedEventHandler(this.dbContext);
            await handler.Handle(notification, default);

            // Assert
            var modelChanges = await this.dbContext.ModelChanges.Find(o => o.ElementId == notification.ElementId);
            Assert.That(modelChanges.Count, Is.EqualTo(1), "There should by only one model change in database.");
        }
    }
}
