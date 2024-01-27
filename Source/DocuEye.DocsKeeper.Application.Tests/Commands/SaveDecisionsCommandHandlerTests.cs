using DocuEye.DocsKeeper.Application.Commads.SaveDecisions;
using DocuEye.DocsKeeper.Application.Commands.SaveDecisions;
using DocuEye.DocsKeeper.Model;

namespace DocuEye.DocsKeeper.Application.Tests.Commands
{
    public class SaveDecisionsCommandHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenHandleSaveDecisionsCommandAndNoDecisionsToSaveDbContainsNoDecisions()
        {
            // Arrange
            var command = new SaveDecisionsCommand("workspacetest1");

            // Act
            var handler = new SaveDecisionsCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.Decisions.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(0), "There should be no decisions in database");
        }

        [Test]
        public async Task WhenHandleSaveDecisionsCommandDbContainsAccurateNumberOfDecisions()
        {
            // Arrange
            var command = new SaveDecisionsCommand("workspacetest1")
            {
                DecisionsToAdd = new List<Decision>()
                {
                    new Decision()
                    {
                        Id = "DecisionId1",
                        WorkspaceId = "workspacetest1",
                    }
                }
            };

            // Act
            var handler = new SaveDecisionsCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.Decisions.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(1), "There should be exact 1 decision in DB");
        }
    }
}
