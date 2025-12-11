using DocuEye.WorkspaceImporter.Api.Model.Maps;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportViewConfiguration
{
    public class ImportViewConfigurationCommandHandler : BaseImportDataCommandHandler, IRequestHandler<ImportViewConfigurationCommand, ImportViewConfigurationResult>
    {
        private readonly IMediator mediator;

        public ImportViewConfigurationCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }
        public async Task<ImportViewConfigurationResult> Handle(ImportViewConfigurationCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ImportViewConfigurationResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            var viewConfiguration = request.ViewConfiguration.MapToViewConfiguration();
            viewConfiguration.Id = request.WorkspaceId;

            await this.mediator.Send(new SaveViewConfigurationCommand()
            {
                ViewConfiguration = viewConfiguration
            });

            return new ImportViewConfigurationResult(request.WorkspaceId, true);
        }
    }
}
