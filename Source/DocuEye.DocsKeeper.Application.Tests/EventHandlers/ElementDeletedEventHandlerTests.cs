using DocuEye.DocsKeeper.Application.EventHandlers;
using DocuEye.WorkspaceImporter.Application.Events;

namespace DocuEye.DocsKeeper.Application.Tests.EventHandlers
{
    public class ElementDeletedEventHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenElementIsDeletedThenAllDocumentationFilesForElementAreDeletedTo()
        {
            // Arrange
            var evt = new ElementDeletedEvent("workspacetest1", "elementtestId1", "","", null, "");

            // Act
            var handler = new ElementDeletedEventHandler(this.dbContext);
            await handler.HandleAsync(evt, default);
            
            // Assert
            var items = await this.dbContext.DocumentationFiles.Find(o => o.WorkspaceId == "workspacetest1" && o.ElementId == "elementtestId1");
            Assert.That(items.Count, Is.EqualTo(0), "There should nno documentation files for element.");

        }
    }
}
