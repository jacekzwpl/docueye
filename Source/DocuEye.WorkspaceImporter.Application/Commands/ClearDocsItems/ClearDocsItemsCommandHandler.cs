using DocuEye.DocsKeeper.Application.Commads.ClearDocumentations;
using DocuEye.DocsKeeper.Application.Commads.ClearImages;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDocsItems
{
    public class ClearDocsItemsCommandHandler : BaseImportDataCommandHandler, IRequestHandler<ClearDocsItemsCommand, ClearDocsItemsResult>
    {
        private readonly IMediator mediator;

        public ClearDocsItemsCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ClearDocsItemsResult> Handle(ClearDocsItemsCommand request, CancellationToken cancellationToken)
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

            await this.mediator.Send(new ClearDocumentationsCommand(request.WorkspaceId));
            await this.mediator.Send(new ClearImagesCommand(request.WorkspaceId));

            return new ClearDocsItemsResult(request.WorkspaceId, true);
        }
    }
}
