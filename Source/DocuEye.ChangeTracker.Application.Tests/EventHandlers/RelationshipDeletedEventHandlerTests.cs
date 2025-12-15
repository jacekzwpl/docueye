using DocuEye.ChangeTracker.Application.EventHandlers;
using DocuEye.WorkspaceImporter.Application.Events;

namespace DocuEye.ChangeTracker.Application.Tests.EventHandlers
{
    public class RelationshipDeletedEventHandlerTests : BaseChangeTrackerTests
    {
        [Test]
        public async Task WhenHandleRelationshipDeletedEventModelChangeIsPresentInDatabase()
        {
            // Arrange
            var notification = new RelationshipDeletedEvent(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                "importkey",
                null,
                "selement",
                "delement"
            );

            // Act
            var handler = new RelationshipDeletedEventHandler(this.dbContext);
            await handler.HandleAsync(notification, default);

            // Assert
            var modelChange = await this.dbContext.ModelChanges.FindOne(o => o.RelationshipId == notification.RelationshipId);
            Assert.That(modelChange, !Is.Null, "Model change event shuold be present in database.");
        }

        [Test]
        public async Task WhenHandleRelationshipDeletedEventForIdenticalImportIdOnlyOneModelChangeIsPresentInDataBase()
        {
            // Arrange
            var notification = new RelationshipDeletedEvent(
                "workspacetest1",
                "relationshiptest1",
                "importtest1",
                "importkey",
                null,
                "selement",
                "delement"
            );

            // Act
            var handler = new RelationshipDeletedEventHandler(this.dbContext);
            await handler.HandleAsync(notification, default);

            // Assert
            var modelChanges = await this.dbContext.ModelChanges.Find(o => o.RelationshipId == notification.RelationshipId);
            Assert.That(modelChanges.Count, Is.EqualTo(1), "There should by only one model change in database.");
        }
    }
}
