using DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Tests.Commands
{
    public class SaveViewConfigurationCommandHandlerTests : BaseWorkspacesKeeperTests
    {
        [Test]
        public async Task WhenSaveViewConfigurationThenDataIsSaved()
        {
            // Arrange
            var query = new SaveViewConfigurationCommand()
            {
                ViewConfiguration = new ViewConfiguration()
                {
                    Id = "workspacetest1",
                    Themes = new string[] { "http://docueye.com" }
                }
            };

            // Act
            var handler = new SaveViewConfigurationCommandHandler(this.dbContext);
            await handler.HandleAsync(query, default);

            // Assert
            var item = await this.dbContext.ViewConfigurations.FindOne(o => o.Id == "workspacetest1");
            Assert.That(item, !Is.Null, "ViewConfiguration should exists.");
            Assert.That(item.Themes?.Count(), Is.EqualTo(1), "ViewConfiguration should contains one theme.");
        }

        [Test]
        public async Task WhenSaveViewConfigurationThatDoesNotExistsThenDataIsSaved()
        {
            // Arrange
            var query = new SaveViewConfigurationCommand()
            {
                ViewConfiguration = new ViewConfiguration()
                {
                    Id = "workspacetest2"
                }
            };

            // Act
            var handler = new SaveViewConfigurationCommandHandler(this.dbContext);
            await handler.HandleAsync(query, default);

            // Assert
            var item = await this.dbContext.ViewConfigurations.FindOne(o => o.Id == "workspacetest2");
            Assert.That(item, !Is.Null, "ViewConfiguration should exists");
        }
    }
}
