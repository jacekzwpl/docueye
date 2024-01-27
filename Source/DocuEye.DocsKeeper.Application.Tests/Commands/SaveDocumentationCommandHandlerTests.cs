using DocuEye.DocsKeeper.Application.Commads.SaveDocumentationChanges;
using DocuEye.DocsKeeper.Application.Commands.SaveDocumentation;
using DocuEye.DocsKeeper.Model;

namespace DocuEye.DocsKeeper.Application.Tests.Commands
{
    public class SaveDocumentationCommandHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenHandleSaveDocumentationsCommandAndNoDocumentationsToSaveDbContainsNoDocumentations()
        {
            // Arrange
            var command = new SaveDocumentationCommand("workspacetest1");

            // Act
            var handler = new SaveDocumentationCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.Documentations.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(0), "There should be no documentation in database");
        }

        [Test]
        public async Task WhenHandleSaveDecisionsCommandDbContainsAccurateNumberOfDecisions()
        {
            // Arrange
            var command = new SaveDocumentationCommand("workspacetest1")
            {
                DocumentationsToAdd = new List<Documentation>()
                {
                    new Documentation()
                    {
                        Id = "DocId1",
                        WorkspaceId = "workspacetest1",
                    }
                }
            };

            // Act
            var handler = new SaveDocumentationCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.Documentations.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(1), "There should be exact 1 documentation in DB");
        }
    }
}
