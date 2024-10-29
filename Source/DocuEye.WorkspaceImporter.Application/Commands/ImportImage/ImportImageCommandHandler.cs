using AutoMapper;
using DocuEye.DocsKeeper.Application.Commads.SaveSingleImage;
using DocuEye.DocsKeeper.Model;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportImage
{
    public class ImportImageCommandHandler : BaseImportDataCommandHandler, IRequestHandler<ImportImageCommand, ImportImageResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ImportImageCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task<ImportImageResult> Handle(ImportImageCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ImportImageResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            var image = this.mapper.Map<Image>(request.Image);
            image.WorkspaceId = request.WorkspaceId;
            image.Id = Guid.NewGuid().ToString();

            // save image
            await this.mediator.Send(new SaveSingleImageCommand(image));

            return new ImportImageResult(request.WorkspaceId, true);
        }
    }
}
