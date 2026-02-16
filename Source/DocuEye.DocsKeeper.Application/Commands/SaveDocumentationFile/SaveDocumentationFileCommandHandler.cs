using DocuEye.DocsKeeper.Application.Commads.SaveDocumentationFile;
using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Commands;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.SaveDocumentationFile
{
    public class SaveDocumentationFileCommandHandler : ICommandHandler<SaveDocumentationFileCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public SaveDocumentationFileCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task HandleAsync(SaveDocumentationFileCommand command, CancellationToken cancellationToken = default)
        {
            var existingFile = await this.dbContext.DocumentationFiles
                .FindOne(o => o.WorkspaceId == command.WorkspaceId
                && o.ElementId == command.ElementId
                && o.Type == command.DocumentationType);

            var documentationFile = new DocumentationFile()
            {
                Id = existingFile == null ? Guid.NewGuid().ToString() : existingFile.Id,
                Content = command.Content,
                ElementId = command.ElementId,
                WorkspaceId = command.WorkspaceId,
                Name = command.Name,
                MediaType = Path.GetExtension(command.Name).ToLower() == ".json" ? "application/json" : "application/yaml",
                Type = command.DocumentationType
            };


            await this.dbContext.DocumentationFiles.UpsertOneAsync(documentationFile);
        }
    }
}
