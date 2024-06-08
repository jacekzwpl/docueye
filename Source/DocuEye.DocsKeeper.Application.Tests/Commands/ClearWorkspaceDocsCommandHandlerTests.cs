using DocuEye.DocsKeeper.Application.Commads.ClearWorkspaceDocs;
using DocuEye.DocsKeeper.Application.Commands.ClearWorkspaceDocs;

namespace DocuEye.DocsKeeper.Application.Tests.Commands
{
    public class ClearWorkspaceDocsCommandHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenClearWorkspaceDocsThenNoDocumentationElementsArePresent()
        {
            // Arrange
            var command = new ClearWorkspaceDocsCommand("workspacetest1");

            // Act
            var handler = new ClearWorkspaceDocsCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var decisions = await this.dbContext.Decisions.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(decisions.Count, Is.EqualTo(0), "There should be no decisions in workspace.");
            var documentationFiles = await this.dbContext.DocumentationFiles.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(documentationFiles.Count, Is.EqualTo(0), "There should be no documentation files in workspace.");
            var images = await this.dbContext.Images.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(images.Count, Is.EqualTo(0), "There should be no images in workspace.");
            var documentations = await this.dbContext.Documentations.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(documentations.Count, Is.EqualTo(0), "There should be no documentations in workspace.");
        }
    }
}
