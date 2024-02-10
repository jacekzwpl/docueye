using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.SaveOpenApiFile
{
    public class SaveOpenApiFileCommandHandler : IRequestHandler<SaveOpenApiFileCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongDB context</param>
        public SaveOpenApiFileCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(SaveOpenApiFileCommand request, CancellationToken cancellationToken)
        {

            var existingFile = await this.dbContext.DocumentationFiles
                .FindOne(o => o.WorkspaceId == request.WorkspaceId
                && o.ElementId == request.ElementId
                && o.Type == DocumentationFileType.OpenApiDefinition);
            var test = Path.GetExtension(request.Name).ToLower();
            var documentationFile = new DocumentationFile()
            {
                Id = existingFile == null ? Guid.NewGuid().ToString() : existingFile.Id,
                Content = request.Content,
                ElementId = request.ElementId,
                WorkspaceId = request.WorkspaceId,
                Name = request.Name,
                MediaType = Path.GetExtension(request.Name).ToLower() == ".json" ? "application/json" : "application/yaml",
                Type = DocumentationFileType.OpenApiDefinition
            };

            
            await this.dbContext.DocumentationFiles.UpsertOneAsync(documentationFile);
        }
    }
}
