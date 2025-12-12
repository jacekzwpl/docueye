using DocuEye.WorkspaceImporter.Api.Model.Docs;
using DocuEye.WorkspaceImporter.Application.Commands.ImportImage;
using DocuEye.WorkspaceImporter.Model;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class ImportImageCommandHandlerTests : BaseCommandTests
    {
        [Test]
        public async Task WhenImportImageAndImportDoNotExistsThenReturnStatusFalse() 
        {
            // Arrange
            var command = new ImportImageCommand(
                "workspace1", 
                "importkey-new", 
                new ImageToImport());
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {

            });

            // Act
            var handler = new ImportImageCommandHandler(this.mediator, dbContext);
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("No import found with key = 'importkey-new'. Start import before continue."), "Message should be 'No import found with key = 'importkey-new'. Start import before continue.'.");

        }

        [Test]
        public async Task WhenImportImageAndImportExistsButIsFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportImageCommand(
                "workspace1",
                "importkey1",
                new ImageToImport());
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {
                new WorkspaceImport()
                {
                    Id = "import1",
                    Key = "importkey1",
                    SourceLink = "sourcelink1",
                    WorkspaceId = "workspace1",
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow
                }
            });

            // Act
            var handler = new ImportImageCommandHandler(this.mediator, dbContext);
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Import with key = 'importkey1' is already finished."), "Message should be 'Import with key = 'importkey1' is already finished.'.");

        }

        [Test]
        public async Task WhenImportImageThenReturnStatusTrue()
        {
            // Arrange
            var command = new ImportImageCommand(
                "workspace1",
                "importkey1",
                new ImageToImport()
                {
                    DocumentationId = "docId",
                    Content = "content",
                    Name = "name",
                    Type = "type"
                });
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {
                new WorkspaceImport()
                {
                    Id = "import1",
                    Key = "importkey1",
                    SourceLink = "sourcelink1",
                    WorkspaceId = "workspace1",
                    StartTime = DateTime.UtcNow
                }
            });

            // Act
            var handler = new ImportImageCommandHandler(this.mediator, dbContext);
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }
    }
}
