using DocuEye.DocsKeeper.Application.Commads.ClearDocumentations;
using DocuEye.DocsKeeper.Application.Commads.ClearImages;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDocsItems
{
    public class ClearDocsItemsCommandHandler : IRequestHandler<ClearDocsItemsCommand, ClearDocsItemsResult>
    {
        private readonly IMediator mediator;
        private readonly IWorkspaceImporterDBContext dbContext;

        public ClearDocsItemsCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
        }

        public async Task<ClearDocsItemsResult> Handle(ClearDocsItemsCommand request, CancellationToken cancellationToken)
        {
            // Get import data
            var import = await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == request.WorkspaceId && o.Key == request.ImportKey);

            // If no import found then stop
            if (import == null)
            {
                return new ClearDocsItemsResult(
                        request.WorkspaceId,
                        false,
                        string.Format("No import found with key = '{0}'. Start import before continue.",
                            request.ImportKey));
            }
            // if import is already finished then stop
            if (import.EndTime != null)
            {
                return new ClearDocsItemsResult(
                        request.WorkspaceId,
                        false,
                        string.Format("Import with key = '{0}' is already finished.",
                            request.ImportKey));
            }

            await this.mediator.Send(new ClearDocumentationsCommand(request.WorkspaceId));
            await this.mediator.Send(new ClearImagesCommand(request.WorkspaceId));

            return new ClearDocsItemsResult(request.WorkspaceId, true);
        }
    }
}
