using DocuEye.ViewsKeeper.Application.Commands.ClearWorkspaceViews;

namespace DocuEye.ViewsKeeper.Application.Tests.Commands
{
    public class ClearWorkspaceViewsCommandHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenClearWorkspaceViewsThereIsNoViewsInWorkspace()
        {
            // Arrange
            var command = new ClearWorkspaceViewsCommand("workspacetest1");

            // Act
            var handler = new ClearWorkspaceViewsCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.AllViews.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(items.Count, Is.EqualTo(0), "There should be no views in workspace");
        }
    }
}
