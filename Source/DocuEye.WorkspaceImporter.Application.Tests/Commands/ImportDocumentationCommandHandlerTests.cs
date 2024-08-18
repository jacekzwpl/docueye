using DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation;
using DocuEye.WorkspaceImporter.Model;
using MediatR;
using Moq;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class ImportDocumentationCommandHandlerTests : BaseCommandTests
    {
        [Test]
        public async Task WhenImportDocumentationAndImportDoNotExistsThenReturnStatusFalse() 
        { 
            // Arrange
            var command = new ImportDocumentationCommand(
                "workspace1", "importkey-new", new DocumentationToImport()
                {
                    Id = "docId",
                });
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {

            });

            // Act
            var handler = new ImportDocumentationCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("No import found with key = 'importkey-new'. Start import before continue."), "Message should be 'No import found with key = 'importkey-new'. Start import before continue.'.");
        }

        [Test]
        public async Task WhenImportDocumentationAndImportExistsButIsFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportDocumentationCommand(
                "workspace1", "importkey1", new DocumentationToImport()
                {
                    Id = "docId",
                });
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
            var handler = new ImportDocumentationCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Import with key = 'importkey1' is already finished."), "Message should be 'Import with key = 'importkey1' is already finished.'.");

        }

        [Test]
        public async Task WhenImportDocumentationForWorkspaceThenReturnStatusTrue()
        {
            // Arrange
            var command = new ImportDocumentationCommand(
                "workspace1", "importkey1", new DocumentationToImport()
                {
                    Id = "docId",
                    Sections = new List<DocumentationSectionToImport>()
                    {
                        new DocumentationSectionToImport()
                        {
                            Content = "content",
                            Format = "markdown"
                        }
                    }
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
            var handler = new ImportDocumentationCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }

        [Test]
        public async Task WhenImportDocumentationForElementThenReturnStatusTrue()
        {
            // Arrange
            var command = new ImportDocumentationCommand(
                "workspace1", "importkey1", new DocumentationToImport()
                {
                    Id = "docId",
                    StructurizrElementId = "1",
                    Sections = new List<DocumentationSectionToImport>()
                    {
                        new DocumentationSectionToImport()
                        {
                            Content = "content",
                            Format = "markdown"
                        }
                    }
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
            var handler = new ImportDocumentationCommandHandler(mediatorMock.Object, this.mapper, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }

        [Test]
        public async Task WhenImportDocumentationForElementThatDoesNotExistsThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportDocumentationCommand(
                "workspace1", "importkey1", new DocumentationToImport()
                {
                    Id = "docId",
                    StructurizrElementId = "1",
                    Sections = new List<DocumentationSectionToImport>()
                    {
                        new DocumentationSectionToImport()
                        {
                            Content = "content",
                            Format = "markdown"
                        }
                    }
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
                .Returns(Task.FromResult<Element?>(null));

            // Act
            var handler = new ImportDocumentationCommandHandler(mediatorMock.Object, this.mapper, dbContext);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Element with structurizr id = '1' not found in workspace"), "Message should be 'Element with structurizr id = '1' not found in workspace'");
        }
    }
}
