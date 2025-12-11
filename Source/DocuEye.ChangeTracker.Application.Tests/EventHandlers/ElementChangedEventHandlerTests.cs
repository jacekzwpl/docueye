using DocuEye.ChangeTracker.Application.EventHandlers;
using DocuEye.WorkspaceImporter.Application.Events;

namespace DocuEye.ChangeTracker.Application.Tests.EventHandlers
{
    public class ElementChangedEventHandlerTests : BaseChangeTrackerTests
    {
        [Test]
        public async Task WhenHandleElementChangedEventModelChangeIsPresentInDatabase()
        {
            // Arrange
            var notification = new ElementChangedEvent(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                null,
                "TestName"
                );

            // Act
            var handler = new ElementChangedEventHandler(this.dbContext);
            await handler.HandleAsync(notification, default);

            // Assert
            var modelChange = await this.dbContext.ModelChanges.FindOne(o => o.ElementId == notification.ElementId);
            Assert.That(modelChange, !Is.Null, "Model change event shuold be present in database.");
        }

        [Test]
        public async Task WhenHandleElementChangedEventForIdenticalImportIdOnlyOneModelChangeIsPresentInDataBase()
        {
            // Arrange
            var notification = new ElementChangedEvent(
                "workspacetest1",
                "elementtest1",
                "importtest1",
                "importkey",
                null,
                "TestName"
                );

            // Act
            var handler = new ElementChangedEventHandler(this.dbContext);
            await handler.HandleAsync(notification, default);

            // Assert
            var modelChanges = await this.dbContext.ModelChanges.Find(o => o.ElementId == notification.ElementId);
            Assert.That(modelChanges.Count, Is.EqualTo(1), "There should by only one model change in database.");
        }
    }
}
