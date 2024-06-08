using DocuEye.ModelKeeper.Application.Commands.ClearWorkspaceModel;
using DocuEye.ModelKeeper.Application.Commands.SaveElements;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Application.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Tests.Commands
{
    public class ClearWorkspaceModelCommandHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenClearModelThenNoElementsOrRelationshipsArePresent()
        {
            // Arrange
            var command = new ClearWorkspaceModelCommand("workspacetest1");

            // Act
            var handler = new ClearWorkspaceModelCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var elements = await this.dbContext.Elements.Find(o => o.WorkspaceId == "workspacetest1");
            var relationships = await this.dbContext.Relationships.Find(o => o.WorkspaceId == "workspacetest1");
            Assert.That(elements.Count, Is.EqualTo(0), "There should be no element with WorkspaceId = workspacetest1  in database");
            Assert.That(relationships.Count, Is.EqualTo(0), "There should be no relationship with WorkspaceId = workspacetest1  in database");
        }
    }
}
