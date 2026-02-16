using DocuEye.DocsKeeper.Application.Commads.SaveDocumentationFile;
using DocuEye.DocsKeeper.Application.Commands.SaveDocumentationFile;
using DocuEye.DocsKeeper.Model;

namespace DocuEye.DocsKeeper.Application.Tests.Commands
{
    internal class SaveDocumentationFileCommandHandlerTests : BaseDocsKeeperTests
    {
        [Test]
        public async Task WhenSaveDocumentationFileThenNewFileIsCreated()
        {
            // Arrange
            var command = new SaveDocumentationFileCommand(
                "workspacetest1", 
                "elementtestId-new", 
                "test", 
                "swagger.json", 
                DocumentationFileType.OpenApiDefinition);
            var oldItems = await this.dbContext.DocumentationFiles.Find(o => true);
            // Act
            var handler = new SaveDocumentationFileCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var items = await this.dbContext.DocumentationFiles.Find(o => true);
            Assert.That(items.Count, Is.GreaterThan(oldItems.Count()), "There should be more elements in collection after save");
        }

        [Test]
        public async Task WhenDocumentationFileExistsForElementAndSaveDocumentationFileThenFileIsReplaced()
        {
            // Arrange
            var command = new SaveDocumentationFileCommand(
                "workspacetest1", 
                "elementtestId1", 
                "test", 
                "swagger.json", 
                DocumentationFileType.OpenApiDefinition);
            var oldItems = await this.dbContext.DocumentationFiles.Find(o => true);
            // Act
            var handler = new SaveDocumentationFileCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var items = await this.dbContext.DocumentationFiles.Find(o => true);
            Assert.That(items.Count, Is.EqualTo(oldItems.Count()), "Number of elements in collection should be the same after save");
        }

        [Test]
        public async Task WhenSaveDocumentationFileWithJsonExtensionThenMediaTypeIsApplicationJson()
        {
            // Arrange
            var command = new SaveDocumentationFileCommand(
                "workspacetest1", 
                "elementtestId1", 
                "test", 
                "swagger.json", 
                DocumentationFileType.OpenApiDefinition);
            // Act
            var handler = new SaveDocumentationFileCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var item = await this.dbContext.DocumentationFiles.FindOne(o => o.ElementId == "elementtestId1");
            Assert.That(item.MediaType, Is.EqualTo("application/json"), "MediaType should be equal to application/json");
        }

        [Test]
        public async Task WhenSaveDocumentationFileWithYamlExtensionThenMediaTypeIsApplicationYaml()
        {
            // Arrange
            var command = new SaveDocumentationFileCommand(
                "workspacetest1", 
                "elementtestId1", 
                "test", 
                "swagger.yaml", 
                DocumentationFileType.OpenApiDefinition);
            // Act
            var handler = new SaveDocumentationFileCommandHandler(this.dbContext);
            await handler.HandleAsync(command, default);

            // Assert
            var item = await this.dbContext.DocumentationFiles.FindOne(o => o.ElementId == "elementtestId1");
            Assert.That(item.MediaType, Is.EqualTo("application/yaml"), "MediaType should be equal to application/yaml");
        }
    }
}
