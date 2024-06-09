using DocuEye.WorkspacesKeeper.Application.Commands.DeleteWorkspace;
using MediatR;
using Moq;

namespace DocuEye.WorkspacesKeeper.Application.Tests.Commands
{
    public class DeleteWorkspaceCommandHandlerTests : BaseWorkspacesKeeperTests
    {
        [Test]
        public async Task WhenWorkspaceDeletedThenNoDataForWorkspaceIsPresent()
        {
            // Arrange
            var command = new DeleteWorkspaceCommand("workspacetest1");

            var mediator = new Mock<IMediator>();
            mediator.Setup(i => i.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

            // Act
            var handler = new DeleteWorkspaceCommandHandler(mediator.Object, dbContext);
            await handler.Handle(command, default);

            // Assert
            var viewConfigurations = await dbContext.ViewConfigurations.Find(o => o.Id == "workspacetest1");
            Assert.That(viewConfigurations.Count, Is.EqualTo(0), "No ViewConfigurations should exists.");
            var workspace = await dbContext.Workspaces.FindOne(o => o.Id == "workspacetest1");
            Assert.That(workspace, Is.Null, "Workspace should not exists.");

        }
    }
}
