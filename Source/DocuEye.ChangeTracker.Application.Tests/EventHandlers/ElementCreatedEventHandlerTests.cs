using DocuEye.ChangeTracker.Application.EventHandlers;
using DocuEye.WorkspaceImporter.Application.Events;

namespace DocuEye.ChangeTracker.Application.Tests.EventHandlers
{
    public class ElementCreatedEventHandlerTests : BaseChangeTrackerTests
    {
        [Test]
        public async Task WhenHandleElementCreatedEventModelChangeIsPresentInDatabase()
        {
            // Arrange
            var notification = new ElementCreatedEvent(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                "elementname",
                Guid.NewGuid().ToString(),
                null);

            // Act
            var handler = new ElementCreatedEventHandler(this.dbContext);
            await handler.Handle(notification, default);

            // Assert
            var modelChange = await this.dbContext.ModelChanges.FindOne(o => o.ElementId == notification.ElementId);
            Assert.That(modelChange, !Is.Null, "Model change event shuold be present in database.");
        }

        [Test]
        public async Task WhenHandleElementCreatedEventForIdenticalImportIdOnlyOneModelChangeIsPresentInDataBase()
        {
            // Arrange
            var notification = new ElementCreatedEvent(
                "workspacetest1", 
                "elementtest1", 
                "importtest1", 
                "elementname", 
                "importkey", null);


            // Act
            var handler = new ElementCreatedEventHandler(this.dbContext);
            await handler.Handle(notification, default);

            // Assert
            var modelChanges = await this.dbContext.ModelChanges.Find(o => o.ElementId == notification.ElementId);
            Assert.That(modelChanges.Count, Is.EqualTo(1), "There should by only one model change in database.");
        }
    }
}
