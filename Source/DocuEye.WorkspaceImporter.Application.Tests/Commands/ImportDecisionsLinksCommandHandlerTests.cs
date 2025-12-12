using DocuEye.DocsKeeper.Application.Model;
using DocuEye.DocsKeeper.Application.Queries.GetDecisionsList;
using DocuEye.DocsKeeper.Application.Queries.GetDecisonByDslId;
using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDecisionsLinks;
using DocuEye.WorkspaceImporter.Model;

using Moq;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class ImportDecisionsLinksCommandHandlerTests : BaseCommandTests
    {
        [Test]
        public async Task WhenImportDecisionLinksAndImportDoNotExistsThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportDecisionsLinksCommand(
                "workspace1", "importkey-new","documentationId", "decisiondslId", new List<DecisionLinkToImport>());

            var dbContext = new FakeDbContext(new List<WorkspaceImport>()
            {

            });

            // Act
            var handler = new ImportDecisionsLinksCommandHandler(this.mediator, dbContext);
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("No import found with key = 'importkey-new'. Start import before continue."), "Message should be 'No import found with key = 'importkey-new'. Start import before continue.'.");
        }

        [Test]
        public async Task WhenImportDecisionLinksAndImportExistsButIsFinishedThenReturnStatusFalse()
        {
            // Arrange
            var command = new ImportDecisionsLinksCommand(
                "workspace1", "importkey1", "documentationId", "decisiondslId", new List<DecisionLinkToImport>());

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
            var handler = new ImportDecisionsLinksCommandHandler(this.mediator, dbContext);
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False, "Status should be false.");
            Assert.That(result.Message, Is.EqualTo("Import with key = 'importkey1' is already finished."), "Message should be 'Import with key = 'importkey1' is already finished.'.");
        }

        [Test]
        public async Task WhenImportDecisionLinksThenReturnsStatusTrue()
        {
            // Arrange
            var command = new ImportDecisionsLinksCommand(
                "workspace1", "importkey1", "documentationId", "decisiondslId", new List<DecisionLinkToImport>()
                {

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
                i => i.SendQueryAsync<GetDecisonByDslIdQuery, Decision?>(It.IsAny<GetDecisonByDslIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns(
                Task.FromResult<Decision?>(new Decision()
                {
                    Id = "decisionId"
                }));
            mediatorMock.Setup(
                i => i.SendQueryAsync<GetDecisionsListQuery, IEnumerable<DecisionsListItem>>(It.IsAny<GetDecisionsListQuery>(), It.IsAny<CancellationToken>()))
                .Returns(
                    Task.FromResult<IEnumerable<DecisionsListItem>>(new List<DecisionsListItem>(){ 
                        new DecisionsListItem()
                        {
                            Id = "decisionId",
                            DslId = "decisionDslId",
                            DocumentationId = "documentationId"
                        }
                    })
                );

            // Act
            var handler = new ImportDecisionsLinksCommandHandler(mediatorMock.Object, dbContext);
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True, "Status should be true.");
            Assert.That(result.Message, Is.Null, "Message should be null.");

        }
    }
}
