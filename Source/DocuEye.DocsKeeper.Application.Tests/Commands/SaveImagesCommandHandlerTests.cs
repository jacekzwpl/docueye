using DocuEye.DocsKeeper.Application.Commads.SaveImages;
using DocuEye.DocsKeeper.Application.Commands.SaveImages;
using DocuEye.DocsKeeper.Model;

namespace DocuEye.DocsKeeper.Application.Tests.Commands
{
    public class SaveImagesCommandHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenHandleSaveImagesCommandAndNoImagesToSaveDbContainsNoImages()
        {
            // Arrange
            var command = new SaveImagesCommand("workspacetest1");

            // Act
            var handler = new SaveImagesCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.Images.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(0), "There should be no image in database");
        }

        [Test]
        public async Task WhenHandleSaveDecisionsCommandDbContainsAccurateNumberOfDecisions()
        {
            // Arrange
            var command = new SaveImagesCommand("workspacetest1")
            {
                ImagesToAdd = new List<Image>()
                {
                    new Image()
                    {
                        Id = "ImageId1",
                        WorkspaceId = "workspacetest1",
                    }
                }
            };

            // Act
            var handler = new SaveImagesCommandHandler(this.dbContext);
            await handler.Handle(command, default);

            // Assert
            var items = await this.dbContext.Images.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(1), "There should be exact 1 image in DB");
        }
    }
}
