using DocuEye.ViewsKeeper.Application.Commands.SaveViewsChanges;
using DocuEye.ViewsKeeper.Application.Queries.FindViewsWithElement;
using DocuEye.ViewsKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Tests.Commands
{
    public class SaveViewsChangesCommandHandlerTests : BaseViewsKeeperTests
    {
        [Test]
        public async Task WhenHandleSaveViewsChangesCommandAndNoViewsToSaveDbContainsNoViews()
        {
            // Arrange
            var command = new SaveViewsChangesCommand("workspacetest1");

            // Act
            var handler = new SaveViewsChangesCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var items = await this.dbContext.AllViews.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(0), "There should be no views in database");
        }

        [Test]
        public async Task WhenHandleSaveViewsChangesCommandDbContainsAccurateNumberOfViews()
        {
            // Arrange
            var command = new SaveViewsChangesCommand("workspacetest1")
            {
                SystemLandscapeViews = new List<SystemLandscapeView>()
                {
                    new SystemLandscapeView()
                    {
                        Id = "SystemLandscapeViewId1",
                        WorkspaceId = "workspacetest1",
                        ViewType = ViewType.SystemLandscapeView
                    }
                }
            };

            // Act
            var handler = new SaveViewsChangesCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var items = await this.dbContext.AllViews.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(1), "There should be exact 1 view in DB");
        }

    }
}
