using DocuEye.WorkspaceImporter.Application.Commands.ClearDocsItems;
using DocuEye.WorkspaceImporter.Model;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class ClearDocsItemsCommandHandlerTests : BaseCommandTests
    {
        [Test]
        public async Task WhenClearDocsItemsAndImportDoNotExistsThenReturnStatusFalse()
        {
            // Arrange
            var command = new ClearDocsItemsCommand("workspace1", "importkey-new");
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {

            });

            // Act
            var handler = new ClearDocsItemsCommandHandler(this.mediator, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("No import found with key = 'importkey-new'. Start import before continue."), "Message should be 'No import found with key = 'importkey-new'. Start import before continue.'.");
        }

        [Test]
        public async Task WhenClearDocsItemsAndImportExistsButIsFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ClearDocsItemsCommand("workspace1", "importkey1");
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
            var handler = new ClearDocsItemsCommandHandler(this.mediator, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Import with key = 'importkey1' is already finished."), "Message should be 'Import with key = 'importkey1' is already finished.'.");
        }

        [Test]
        public async Task WhenClearDocsItemsThenReturnStatusTrue()
        {
            // Arrange
            var command = new ClearDocsItemsCommand("workspace1", "importkey1");
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {
                new WorkspaceImport()
                {
                    Id = "import1",
                    Key = "importkey1",
                    SourceLink = "sourcelink1",
                    WorkspaceId = "workspace1",
                    StartTime = DateTime.UtcNow,
                    EndTime = null
                }
            });

            // Act
            var handler = new ClearDocsItemsCommandHandler(this.mediator, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }
    }
}
