using DocuEye.WorkspacesKeeper.Application.Commands.ClearWorkspaceData;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Tests.Commands
{
    public class ClearWorkspaceDataCommandHandlerTests : BaseWorkspacesKeeperTests
    {
        [Test]
        public async Task WhenClearWorkspaceDataThenNoDataForWorkspaceIsPresent()
        {
            // Arrange
            var command = new ClearWorkspaceDataCommand("workspacetest1");

            // Act
            var handler = new ClearWorkspaceDataCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var viewConfigurations = await this.dbContext.ViewConfigurations.Find(o => o.Id == "workspacetest1");
            Assert.That(viewConfigurations.Count, Is.EqualTo(0), "No ViewConfigurations should exists.");
            var workspace = await this.dbContext.Workspaces.FindOne(o => o.Id == "workspacetest1");
            Assert.That(workspace, Is.Null, "Workspace should not exists.");
            
        }
    }
}
