using AutoMapper;
using DocuEye.DocsKeeper.Application.Commads.SaveSingleDocumentation;
using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation
{
    public class ImportDocumentationCommandHandler : BaseImportDataCommandHandler, IRequestHandler<ImportDocumentationCommand, ImportDocumentationResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public ImportDocumentationCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<ImportDocumentationResult> Handle(ImportDocumentationCommand request, CancellationToken cancellationToken)
        {
            // Chekc import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if(!checkImport.IsSuccess)
            {
                return new ImportDocumentationResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            var documentation = this.mapper.Map<Documentation>(request.Documentation);
            documentation.WorkspaceId = request.WorkspaceId;

            if (!string.IsNullOrEmpty(request.Documentation.StructurizrElementId)) { 
                
                var element = await this.mediator.Send(
                    new GetElementByStructurizrIdQuery(
                        request.WorkspaceId, 
                        request.Documentation.StructurizrElementId));
                if (element != null)
                {
                    documentation.ElementId = element.Id;
                }else
                {
                    return new ImportDocumentationResult(
                        request.WorkspaceId,
                        false,
                        string.Format("Element with structurizr id = '{0}' not found in workspace", request.Documentation.StructurizrElementId));
                }
            }

            // save docs
            await this.mediator.Send(new SaveSingleDocumentationCommand(documentation));

            return new ImportDocumentationResult(request.WorkspaceId, true);
        }
    }
}
