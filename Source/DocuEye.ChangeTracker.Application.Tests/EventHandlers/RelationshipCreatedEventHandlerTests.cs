using DocuEye.ChangeTracker.Application.EventHandlers;
using DocuEye.WorkspaceImporter.Application.Events;

namespace DocuEye.ChangeTracker.Application.Tests.EventHandlers
{
    public class RelationshipCreatedEventHandlerTests : BaseChangeTrackerTests
    {
        [Test]
        public async Task WhenHandleRelationshipCreatedEventModelChangeIsPresentInDatabase()
        {
            // Arrange
            var notification = new RelationshipCreatedEvent(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                "importkey",
                null,
                "selement",
                "delement"
             );

            // Act
            var handler = new RelationshipCreatedEventHandler(this.dbContext);
            await handler.HandleAsync(notification, default);

            // Assert
            var modelChange = await this.dbContext.ModelChanges.FindOne(o => o.RelationshipId == notification.RelationshipId);
            Assert.That(modelChange, !Is.Null, "Model change event shuold be present in database.");
        }

        [Test]
        public async Task WhenHandleRelationshipCreatedEventForIdenticalImportIdOnlyOneModelChangeIsPresentInDataBase()
        {
            // Arrange

            var notification = new RelationshipCreatedEvent(
                "workspacetest1",
                "relationshiptest1",
                "importtest1",
                "importkey",
                null,
                "selement",
                "delement"
            );


            // Act
            var handler = new RelationshipCreatedEventHandler(this.dbContext);
            await handler.HandleAsync(notification, default);

            // Assert
            var modelChanges = await this.dbContext.ModelChanges.Find(o => o.RelationshipId == notification.RelationshipId);
            Assert.That(modelChanges.Count, Is.EqualTo(1), "There should by only one model change in database.");
        }
    }
}
