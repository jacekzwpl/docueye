using DocuEye.ModelKeeper.Application.Commands.SaveRelationships;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Application.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Tests.Commands
{
    public class SaveRelationshipsCommandHandlerTests : BaseModelKeeperTests
    {
        [Test]
        public async Task WhenRelationshipIsDeletedThenIsNotPresentInDatabase()
        {
            // Arrange
            var command = new SaveRelationshipsCommand()
            {
                RelationshipsToDelete = new List<Relationship>()
                {
                   new Relationship()
                   {
                       Id = "RelationshipId1"
                   }
                }
            };

            // Act
            var handler = new SaveRelationshipsCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.Relationships.Find(o => o.Id == "RelationshipId1");
            Assert.That(items.Count, Is.EqualTo(0), "There should be no relationship with ID = RelationshipId1  in database");
        }

        [Test]
        public async Task WhenRelationshipIsCreatedThenIsPresentInDatabase()
        {
            // Arrange
            var command = new SaveRelationshipsCommand()
            {
                RelationshipsToAdd = new List<Relationship>()
                {
                   new Relationship()
                    {
                        Id = "RelationshipId23",
                        SourceId = "ContainerId3",
                        WorkspaceId = "workspacetest1",
                        DestinationId = "ContainerId4"
                    }
                }
            };

            // Act
            var handler = new SaveRelationshipsCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.Relationships.Find(o => o.Id == "RelationshipId23");
            Assert.That(items.Count, Is.EqualTo(1), "There should be 1 relationship with ID = RelationshipId23  in database");

        }

        [Test]
        public async Task WhenRelationshipIsChnagedThenItsPropertiesAreChnaged()
        {
            // Arrange
            var command = new SaveRelationshipsCommand()
            {
                RelationshipsToChange = new List<Relationship>()
                {
                   new Relationship()
                   {
                        Id = "RelationshipId1",
                        WorkspaceId = "workspacetest1",
                        SourceId = "ContainerId3",
                        DestinationId = "ContainerId1",
                        Technology = "TECH"
                   }  
                }
            };

            // Act
            var handler = new SaveRelationshipsCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var item = await this.dbContext.Relationships.FindOne(o => o.Id == "RelationshipId1");
            Assert.That(item.Technology, Is.EqualTo("TECH"), "Technology of relationship has wrong value after update.");
        }
    }
}
