using DocuEye.DocsKeeper.Application.EventHandlers;
using DocuEye.WorkspacesKeeper.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Tests.EventHandlers
{
    public class WorkspaceDeletedEventHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenWorkspaceDeletedThenNoDataForWorkspaceIsPresent()
        {
            // Arrange
            var notification = new WorkspaceDeletedEvent("workspacetest1");

            // Act
            var handler = new WorkspaceDeletedEventHandler(this.dbContext);
            await handler.Handle(notification, default);

            // Assert
            var decisions = await this.dbContext.Decisions.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(decisions.Count, Is.EqualTo(0), "There should be no decisions in workspace.");
            var documentationFiles = await this.dbContext.DocumentationFiles.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(documentationFiles.Count, Is.EqualTo(0), "There should be no documentation files in workspace.");
            var images = await this.dbContext.Images.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(images.Count, Is.EqualTo(0), "There should be no images in workspace.");
            var documentations = await this.dbContext.Documentations.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(documentations.Count, Is.EqualTo(0), "There should be no documentations in workspace.");

        }
    }
}
