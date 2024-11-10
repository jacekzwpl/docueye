using DocuEye.WorkspaceImporter.Application.Commands.StartImport;
using DocuEye.WorkspaceImporter.Model;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class StartImportCommandHandlerTests : BaseCommandTests
    {
        [Test]
        public async Task WhenStartNewImportWhenPreviousIsNotFinishedThenRetrunStatusFalse()
        {
            // Arrange
            var command = new StartImportCommand()
            {
                ImportKey = "importkey-new",
                SourceLink = "sourcelink1",
                WorkspaceName = "workspacename1",
                WorkspaceId = "workspace1",
                WorkspaceDescription = "workspacedescription1"
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
            var handler = new StartImportCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Can not start import because another import with key = 'importkey1' is currently running."), "Message should be 'Can not start import because another import with key = 'importkey1' is currently running.'.");
        }

        [Test]
        public async Task WhenStartImportWithSameKeyAsCurrentlyRunningImportRetrunStatusTrue() { 
        
            // Arrange
            var command = new StartImportCommand()
            {
                ImportKey = "importkey1",
                SourceLink = "sourcelink1",
                WorkspaceName = "workspacename1",
                WorkspaceId = "workspace1",
                WorkspaceDescription = "workspacedescription1"
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
            var handler = new StartImportCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }

        [Test]
        public async Task WhenStartImportWitchIsAlreadyFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new StartImportCommand()
            {
                ImportKey = "importkey1",
                SourceLink = "sourcelink1",
                WorkspaceName = "workspacename1",
                WorkspaceId = "workspace1",
                WorkspaceDescription = "workspacedescription1"
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
            var handler = new StartImportCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Can not start import because another import with key = 'importkey1' is already completed."), "Message should be 'Can not start import because another import with key = 'importkey1' is already completed.'.");
        }

        [Test]
        public async Task WhenStartImportThenReturnStatusTrue()
        {

            // Arrange
            var command = new StartImportCommand()
            {
                ImportKey = "importkey1",
                SourceLink = "sourcelink1",
                WorkspaceName = "workspacename1",
                WorkspaceId = "workspace1",
                WorkspaceDescription = "workspacedescription1"
            };

            var dbContext = new FakeDbContext(new List<WorkspaceImport>());


            // Act
            var handler = new StartImportCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }
    }
}
