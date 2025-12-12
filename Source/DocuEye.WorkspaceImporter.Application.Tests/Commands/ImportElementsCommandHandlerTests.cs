using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Application.Commands.ImportElements;
using DocuEye.WorkspaceImporter.Model;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class ImportElementsCommandHandlerTests : BaseCommandTests
    {
        [Test]
        public async Task WhenImportElementsAndImportDoNotExistsThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportElementsCommand()
            {
                ImportKey = "importkey-new",
                WorkspaceId = "workspace1"
            };
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {

            });

            // Act
            var handler = new ImportElementsCommandHandler(this.mediator, dbContext);
            var result = await handler.HandleAsync(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("No import found with key = 'importkey-new'. Start import before continue."), "Message should be 'No import found with key = 'importkey-new'. Start import before continue.'.");
        }

        [Test]
        public async Task WhenImportElementsAndImportExistsButIsFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportElementsCommand()
            {
                ImportKey = "importkey1",
                WorkspaceId = "workspace1"
            };
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
            var handler = new ImportElementsCommandHandler(this.mediator, dbContext);
            var result = await handler.HandleAsync(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Import with key = 'importkey1' is already finished."), "Message should be 'Import with key = 'importkey1' is already finished.'.");
        }

        [Test]
        public async Task WhenImportElementsThenReturnStatusTrue()
        {
            // Arrange
            var command = new ImportElementsCommand()
            {
                ImportKey = "importkey1",
                WorkspaceId = "workspace1",
                Elements = new List<ElementToImport>()
                {
                    new ElementToImport
                    {
                        StructurizrId = "1",
                        DslId = "softwaresystem1",
                        Type = ElementType.SoftwareSystem,
                        Name = "Software System 1"
                    }
                }
            };
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
            var handler = new ImportElementsCommandHandler(this.mediator, dbContext);
            var result = await handler.HandleAsync(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }
    }
}
