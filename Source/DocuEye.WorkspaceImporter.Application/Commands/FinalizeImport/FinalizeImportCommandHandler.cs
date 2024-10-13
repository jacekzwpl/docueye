using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.FinalizeImport
{
    public class FinalizeImportCommandHandler : BaseImportDataCommandHandler, IRequestHandler<FinalizeImportCommand, FinalizeImportResult>
    {
        public FinalizeImportCommandHandler(IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<FinalizeImportResult> Handle(FinalizeImportCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new FinalizeImportResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            import.EndTime = DateTime.UtcNow;

            await this.dbContext.WorkspaceImports.UpsertOneAsync(import);

            return new FinalizeImportResult(request.WorkspaceId, true);
        }
    }
}
