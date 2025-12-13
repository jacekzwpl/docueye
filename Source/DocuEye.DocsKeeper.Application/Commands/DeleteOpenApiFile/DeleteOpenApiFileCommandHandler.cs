using DocuEye.DocsKeeper.Application.Commads.DeleteOpenApiFile;
using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.DeleteOpenApiFile
{
    public class DeleteOpenApiFileCommandHandler : ICommandHandler<DeleteOpenApiFileCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongDB context</param>
        public DeleteOpenApiFileCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task HandleAsync(DeleteOpenApiFileCommand command, CancellationToken cancellationToken = default)
        {
            var existingFile = await this.dbContext.DocumentationFiles
                .FindOne(o => o.WorkspaceId == command.WorkspaceId
                && o.ElementId == command.ElementId
                && o.Type == DocumentationFileType.OpenApiDefinition);

            if (existingFile != null) {
                await this.dbContext.DocumentationFiles.DeleteOneAsync(o => o.Id == existingFile.Id);
            }
        }
    }
}
