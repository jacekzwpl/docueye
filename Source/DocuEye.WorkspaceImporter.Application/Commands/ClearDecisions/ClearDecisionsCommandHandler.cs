using DocuEye.DocsKeeper.Application.Commads.ClearOldDecisions;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDecisions
{
    public class ClearDecisionsCommandHandler: BaseImportDataCommandHandler, IRequestHandler<ClearDecisionsCommand, ClearDecisionsResult>
    {
        private readonly IMediator mediator;

        public ClearDecisionsCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ClearDecisionsResult> Handle(ClearDecisionsCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ClearDecisionsResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            await this.mediator
                .Send(new ClearOldDecisionsCommand(request.WorkspaceId, request.ImportKey));


            return new ClearDecisionsResult(request.WorkspaceId, true);

        }
    }
}
