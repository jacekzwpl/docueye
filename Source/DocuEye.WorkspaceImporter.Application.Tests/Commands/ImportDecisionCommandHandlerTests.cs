using DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDecision;
using DocuEye.WorkspaceImporter.Model;
using MediatR;
using Moq;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class ImportDecisionCommandHandlerTests : BaseCommandTests
    {
        [Test]
        public async Task WhenImportDecisionAndImportDoNotExistsThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportDecisionCommand(
                "workspace1", "importkey-new", new DecisionToImport());
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {

            });

            // Act
            var handler = new ImportDecisionCommandHandler(this.mediator, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("No import found with key = 'importkey-new'. Start import before continue."), "Message should be 'No import found with key = 'importkey-new'. Start import before continue.'.");
        }

        [Test]
        public async Task WhenImportDecisionAndImportExistsButIsFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportDecisionCommand(
                "workspace1", "importkey1", new DecisionToImport());
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
            var handler = new ImportDecisionCommandHandler(this.mediator, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Import with key = 'importkey1' is already finished."), "Message should be 'Import with key = 'importkey1' is already finished.'.");
        }

        [Test]
        public async Task WhenImportDecisionForWorkspaceThenReturnsStatusTrue()
        {
            // Arrange
            var command = new ImportDecisionCommand(
                "workspace1", "importkey1", new DecisionToImport()
                {
                    DocumentationId = "docId",
                    StrucuturizrId = "strucuturizrId",
                    Date = "2024-11-17",
                    Status = "status",
                    Title = "title",
                    Content = "content",
                    Format = "format"

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
            var handler = new ImportDecisionCommandHandler(this.mediator, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }

        [Test]
        public async Task WhenImportDecisionForElementTheReturnsStatusTrue()
        {
            // Arrange
            var command = new ImportDecisionCommand(
                "workspace1", "importkey1", new DecisionToImport()
                {
                    DocumentationId = "docId",
                    StrucuturizrId = "strucuturizrId",
                    Date = "2024-11-17",
                    Status = "status",
                    Title = "title",
                    Content = "content",
                    Format = "format",
                    StrucuturizrElementId = "elementId"

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

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(
                i => i.Send(It.IsAny<GetElementByStructurizrIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Element?>(new Element()
                {
                    Id = "element"
                }));

            // Act
            var handler = new ImportDecisionCommandHandler(mediatorMock.Object, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }
    }
}
