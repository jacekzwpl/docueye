using AutoMapper;
using DocuEye.DocsKeeper.Application.Commads.SaveSingleImage;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportViewConfiguration
{
    public class ImportViewConfigurationCommandHandler : BaseImportDataCommandHandler, IRequestHandler<ImportViewConfigurationCommand, ImportViewConfigurationResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ImportViewConfigurationCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
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

            var viewConfiguration = this.mapper.Map<ViewConfiguration>(request.ViewConfiguration);
            viewConfiguration.Id = request.WorkspaceId;

            await this.mediator.Send(new SaveViewConfigurationCommand()
            {
                ViewConfiguration = viewConfiguration
            });

            return new ImportViewConfigurationResult(request.WorkspaceId, true);
        }
    }
}
