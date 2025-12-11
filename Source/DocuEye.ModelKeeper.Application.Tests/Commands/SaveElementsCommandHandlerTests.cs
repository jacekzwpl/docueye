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
    public class SaveElementsCommandHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenElementIsDeletedThenIsNotPresentInDatabase()
        {
            // Arrange
            var command = new SaveElementsCommand()
            {
                ElementsToDelete = new List<Element>()
                {
                   new Element()
                   {
                       Id = "SoftwareSystemId2"
                   }
                }
            };

            // Act
            var handler = new SaveElementsCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var items = await this.dbContext.Elements.Find(o => o.Id == "SoftwareSystemId2");
            Assert.That(items.Count, Is.EqualTo(0), "There should be no element with ID = SoftwareSystemId2  in database");
        }

        [Test]
        public async Task WhenElementIsCreatedThenIsPresentInDatabase()
        {
            // Arrange
            var command = new SaveElementsCommand()
            {
                ElementsToAdd = new List<Element>()
                {
                   new Element()
                    {
                        Id = "SoftwareSystemId3",
                        Name = "My System 3",
                        WorkspaceId = "workspacetest1",
                        Type = ElementType.SoftwareSystem
                    }
                }
            };

            // Act
            var handler = new SaveElementsCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var items = await this.dbContext.Elements.Find(o => o.Id == "SoftwareSystemId3");
            Assert.That(items.Count, Is.EqualTo(1), "There should be 1 element with ID = SoftwareSystemId3  in database");

        }

        [Test]
        public async Task WhenElementIsChnagedThenItsPropertiesAreChnaged()
        {
            // Arrange
            var command = new SaveElementsCommand()
            {
                ElementsToChange = new List<Element>()
                {
                   new Element()
                    {
                        Id = "SoftwareSystemId2",
                        Name = "My System 2 chnaged",
                        WorkspaceId = "workspacetest1",
                        Type = ElementType.SoftwareSystem
                    }
                }
            };

            // Act
            var handler = new SaveElementsCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var item = await this.dbContext.Elements.FindOne(o => o.Id == "SoftwareSystemId2");
            Assert.That(item.Name, Is.EqualTo("My System 2 chnaged"), "Name of element has wrong value after update.");
        }
    }
}
