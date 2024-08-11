using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Application.Commands.ImportElements;
using DocuEye.WorkspaceImporter.Application.Commands.ImportRelationships;
using DocuEye.WorkspaceImporter.Model;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class ImportRelationshipsCommandHandlerTests : BaseCommandTests
    {

        [Test]
        public async Task WhenImportRelationshipsAndImportDoNotExistsThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportRelationshipsCommand()
            {
                ImportKey = "importkey-new",
                WorkspaceId = "workspace1"
            };
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {

            });

            // Act
            var handler = new ImportRelationshipsCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("No import found with key = 'importkey-new'. Start import before continue."), "Message should be 'No import found with key = 'importkey-new'. Start import before continue.'.");
        }

        [Test]
        public async Task WhenImportRelationshipsAndImportExistsButIsFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportRelationshipsCommand()
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
            var handler = new ImportRelationshipsCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Import with key = 'importkey1' is already finished."), "Message should be 'Import with key = 'importkey1' is already finished.'.");
        }

        [Test]
        public async Task WhenImportRelationshipsForCleanWorkspaceThenReturnStatusTrue()
        {
            // Arrange
            var command = new ImportRelationshipsCommand()
            {
                ImportKey = "importkey1",
                WorkspaceId = "workspace1",
                Relationships = new List<RelationshipToImport>()
                {
                    new RelationshipToImport
                    {
                        StructurizrId = "1",
                        DslId = "softwaresystem1",
                        StructurizrSourceId = "1",
                        StructurizrDestinationId = "2",
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

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(
                i => i.Send(It.IsAny<GetAllWorkspaceElementsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<IEnumerable<Element>>(new List<Element>()
                {
                    new Element()
                    {
                        Id = "elementId1",
                        StructurizrId = "1",
                    },
                    new Element()
                    {
                        Id = "elementId2",
                        StructurizrId = "2",
                    }
                }));

            // Act
            var handler = new ImportRelationshipsCommandHandler(mediatorMock.Object, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }

        [Test]
        public async Task WhenImportRelationshipsForExistingWorkspaceThenReturnStatusTrue()
        {
            // Arrange
            var command = new ImportRelationshipsCommand()
            {
                ImportKey = "importkey1",
                WorkspaceId = "workspace1",
                Relationships = new List<RelationshipToImport>()
                {
                    new RelationshipToImport
                    {
                        StructurizrId = "1",
                        DslId = "relationshipDslId1",
                        StructurizrSourceId = "1",
                        StructurizrDestinationId = "2",
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

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(
                i => i.Send(It.IsAny<GetAllWorkspaceElementsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<IEnumerable<Element>>(new List<Element>()
                {
                    new Element()
                    {
                        Id = "elementId1",
                        StructurizrId = "1",
                    },
                    new Element()
                    {
                        Id = "elementId2",
                        StructurizrId = "2",
                    }
                }));

            mediatorMock.Setup(
                i => i.Send(It.IsAny<GetAllWorkspaceRelationshipsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<IEnumerable<Relationship>>(new List<Relationship>()
                {
                    new Relationship()
                    {
                        Id = "relationshipId1",
                        StructurizrId = "1",
                        DslId = "relationshipDslId1",
                        SourceId = "elementId1",
                        DestinationId = "elementId2",
                    }
                }));

            // Act
            var handler = new ImportRelationshipsCommandHandler(mediatorMock.Object, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }

       
    }
}
