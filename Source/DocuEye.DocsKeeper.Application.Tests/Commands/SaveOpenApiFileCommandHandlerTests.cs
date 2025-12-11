using DocuEye.DocsKeeper.Application.Commands.SaveOpenApiFile;

namespace DocuEye.DocsKeeper.Application.Tests.Commands
{
    public class SaveOpenApiFileCommandHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenSaveOpenApiFileThenNewFileIsCreated()
        {
            // Arrange
            var command = new SaveOpenApiFileCommand("workspacetest1","elementtestId-new", "test", "swagger.json");
            var oldItems = await this.dbContext.DocumentationFiles.Find(o => true);
            // Act
            var handler = new SaveOpenApiFileCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var items = await this.dbContext.DocumentationFiles.Find(o => true);
            Assert.That(items.Count, Is.GreaterThan(oldItems.Count()), "There should be more elements in collection after save");
        }

        [Test]
        public async Task WhenOpenApiFileExistsForElementAndSaveOpenApiFileThenFileIsReplaced()
        {
            // Arrange
            var command = new SaveOpenApiFileCommand("workspacetest1", "elementtestId1", "test", "swagger.json");
            var oldItems = await this.dbContext.DocumentationFiles.Find(o => true);
            // Act
            var handler = new SaveOpenApiFileCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var items = await this.dbContext.DocumentationFiles.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(oldItems.Count()), "Number of elements in collection should be the same after save");
        }

        [Test]
        public async Task WhenSaveOpenApiFileWithJsonExtensionThenMediaTypeIsApplicationJson()
        {
            // Arrange
            var command = new SaveOpenApiFileCommand("workspacetest1", "elementtestId1", "test", "swagger.json");
            // Act
            var handler = new SaveOpenApiFileCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var item = await this.dbContext.DocumentationFiles.FindOne(o => o.ElementId == "elementtestId1");
            Assert.That(item.MediaType, Is.EqualTo("application/json"), "MediaType should be equal to application/json");
        }

        [Test]
        public async Task WhenSaveOpenApiFileWithYamlExtensionThenMediaTypeIsApplicationYaml()
        {
            // Arrange
            var command = new SaveOpenApiFileCommand("workspacetest1", "elementtestId1", "test", "swagger.yaml");
            // Act
            var handler = new SaveOpenApiFileCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var item = await this.dbContext.DocumentationFiles.FindOne(o => o.ElementId == "elementtestId1");
            Assert.That(item.MediaType, Is.EqualTo("application/yaml"), "MediaType should be equal to application/yaml");
        }
    }
}
