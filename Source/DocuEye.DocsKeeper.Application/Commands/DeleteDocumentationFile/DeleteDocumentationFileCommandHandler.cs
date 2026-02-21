using DocuEye.DocsKeeper.Application.Commads.DeleteDocumentationFile;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Commands.DeleteDocumentationFile
{
    public class DeleteDocumentationFileCommandHandler : ICommandHandler<DeleteDocumentationFileCommand>
    {
        private readonly IDocsKeeperDBContext dbContext;

        public DeleteDocumentationFileCommandHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task HandleAsync(DeleteDocumentationFileCommand command, CancellationToken cancellationToken = default)
        {
            var existingFile = await this.dbContext.DocumentationFiles
                .FindOne(o => o.WorkspaceId == command.WorkspaceId
                && o.ElementId == command.ElementId
                && o.Type == command.DocumentationType);

            if (existingFile != null)
            {
                await this.dbContext.DocumentationFiles.DeleteOneAsync(o => o.Id == existingFile.Id);
            }
        }
    }
}
