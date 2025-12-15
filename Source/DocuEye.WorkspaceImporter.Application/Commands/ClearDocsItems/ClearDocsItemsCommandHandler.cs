using DocuEye.DocsKeeper.Application.Commads.ClearDocumentations;
using DocuEye.DocsKeeper.Application.Commads.ClearImages;
using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation;
using DocuEye.WorkspaceImporter.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDocsItems
{
    public class ClearDocsItemsCommandHandler : BaseImportDataCommandHandler, ICommandHandler<ClearDocsItemsCommand, ClearDocsItemsResult>
    {
        private readonly IMediator mediator;

        public ClearDocsItemsCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ClearDocsItemsResult> HandleAsync(ClearDocsItemsCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ClearDocsItemsResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            await this.mediator.SendCommandAsync(new ClearDocumentationsCommand(request.WorkspaceId));
            await this.mediator.SendCommandAsync(new ClearImagesCommand(request.WorkspaceId));

            return new ClearDocsItemsResult(request.WorkspaceId, true);
        }
    }
}
