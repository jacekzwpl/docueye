using DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace;
using DocuEye.WorkspacesKeeper.Model;

namespace DocuEye.WorkspacesKeeper.Application.Tests.Commands
{
    public class SaveWorkspaceCommandHandlerTests : BaseWorkspacesKeeperTests
    {
        [Test]
        public async Task WhenSaveWorkspaceThenDataIsSaved()
        {
            // Arrange
            var query = new SaveWorkspaceCommand()
            {
                Workspace = new Workspace()
                {
                    Id = "workspacetest1",
                    Name = "My Workspace changed name"
                }
            };

            // Act
            var handler = new SaveWorkspaceCommandHandler(this.dbContext);
            await handler.Handle(query, default);

            // Assert
            var item = await this.dbContext.Workspaces.FindOne(o => o.Id == "workspacetest1");
            Assert.That(item, !Is.Null, "Workspace should exists.");
            Assert.That(item.Name, Is.EqualTo("My Workspace changed name"), "Workspace name is not chnaged");
        }

        [Test]
        public async Task WhenSaveWorkspaceThatDoesNotExistsThenDataIsSaved()
        {
            // Arrange
            var query = new SaveWorkspaceCommand()
            {
                Workspace = new Workspace()
                {
                    Id = "workspacetest2",
                    Name = "My created Workspace"
                }
            };

            // Act
            var handler = new SaveWorkspaceCommandHandler(this.dbContext);
            await handler.Handle(query, default);

            // Assert
            var item = await this.dbContext.Workspaces.FindOne(o => o.Id == "workspacetest2");
            Assert.That(item, !Is.Null, "Workspace should exists");
        }
    }
}
