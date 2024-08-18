using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Application.Queries.GetAllViews;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.WorkspaceImporter.Application.Commands.ImportViews;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspacesKeeper.Application.Queries.GetWorkspace;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class ImportViewsCommandHandlerTests : BaseCommandTests
    {
        [Test]
        public async Task WhenImportViewsAndImportDoNotExistsThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportViewsCommand()
            {
                ImportKey = "importkey-new",
                WorkspaceId = "workspace1"
            };
            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {

            });

            // Act
            var handler = new ImportViewsCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("No import found with key = 'importkey-new'. Start import before continue."), "Message should be 'No import found with key = 'importkey-new'. Start import before continue.'.");
        }
        [Test]
        public async Task WhenImportViewsAndImportExistsButIsFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportViewsCommand()
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
            var handler = new ImportViewsCommandHandler(this.mediator, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Import with key = 'importkey1' is already finished."), "Message should be 'Import with key = 'importkey1' is already finished.'.");
        }
        [Test]
        public async Task WhenImportViewsAndWorkspaceIsNotCreatedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportViewsCommand()
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
                    StartTime = DateTime.UtcNow
                }
            });

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(
                i => i.Send(It.IsAny<GetWorkspaceQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Workspace?>(null));
            // Act
            var handler = new ImportViewsCommandHandler(mediatorMock.Object, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Workspace with ID = 'workspace1' not exists."), "Message should be 'Workspace with ID = 'workspace1' not exists.'.");
        }
        [Test]
        public async Task WhenImportViewsForCleanWorkspaceThenReturnStatusTrue()
        {
            // Arrange
            var command = new ImportViewsCommand()
            {
                ImportKey = "importkey1",
                WorkspaceId = "workspace1",
                Views = new List<ViewToImport>()
                {
                    new ViewToImport
                    {
                        StructurizrElementId = "1",
                        Key = "key1",
                        ViewType = ViewType.SystemLandscapeView,
                        Description = "description1",
                        Elements = new List<ElementInViewToImport>()
                        {
                            new ElementInViewToImport()
                            {
                                StructurizrId = "1",
                                X = 1,
                                Y = 1
                            },
                            new ElementInViewToImport()
                            {
                                StructurizrId = "2",
                                X = 2,
                                Y = 2
                            }
                        },
                        Relationships = new List<RelationshipInViewToImport>()
                        {
                            new RelationshipInViewToImport()
                            {
                                StructurizrId = "1",
                                Description = "description1",
                            }
                        }
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
                i => i.Send(It.IsAny<GetWorkspaceQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Workspace?>(new Workspace()
                {
                    Id = "workspace1",
                    Name = "workspace1"
                 
                }));

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

            mediatorMock.Setup(
                i => i.Send(It.IsAny<GetAllViewsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<IEnumerable<BaseView>>(new List<BaseView>()));

            // Act
            var handler = new ImportViewsCommandHandler(mediatorMock.Object, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }

        [Test]
        public async Task WhenImportViewsForAlreadyFiledWorkspaceThenReturnStatusTrue()
        {
            // Arrange
            var command = new ImportViewsCommand()
            {
                ImportKey = "importkey1",
                WorkspaceId = "workspace1",
                Views = new List<ViewToImport>()
                {
                    new ViewToImport
                    {
                        StructurizrElementId = "1",
                        Key = "key1",
                        ViewType = ViewType.SystemLandscapeView,
                        Description = "description1",
                        Elements = new List<ElementInViewToImport>()
                        {
                            new ElementInViewToImport()
                            {
                                StructurizrId = "1",
                                X = 1,
                                Y = 1
                            },
                            new ElementInViewToImport()
                            {
                                StructurizrId = "2",
                                X = 2,
                                Y = 2
                            }
                        },
                        Relationships = new List<RelationshipInViewToImport>()
                        {
                            new RelationshipInViewToImport()
                            {
                                StructurizrId = "1",
                                Description = "description1",
                            }
                        }
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
                i => i.Send(It.IsAny<GetWorkspaceQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Workspace?>(new Workspace()
                {
                    Id = "workspace1",
                    Name = "workspace1"

                }));

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

            mediatorMock.Setup(
                i => i.Send(It.IsAny<GetAllViewsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<IEnumerable<BaseView>>(new List<BaseView>()
                {
                    new BaseView()
                    {
                        Key = "key1",
                        ViewType = ViewType.SystemLandscapeView,
                    }
                }));

            // Act
            var handler = new ImportViewsCommandHandler(mediatorMock.Object, this.mapper, dbContext);
            var result = await handler.Handle(command, default);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");
        }
    }
}
