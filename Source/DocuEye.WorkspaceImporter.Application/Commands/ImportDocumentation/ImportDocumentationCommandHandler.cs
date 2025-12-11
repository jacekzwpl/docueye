using DocuEye.DocsKeeper.Application.Commads.SaveSingleDocumentation;
using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Maps;
using DocuEye.WorkspaceImporter.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation
{
    public class ImportDocumentationCommandHandler : BaseImportDataCommandHandler, ICommandHandler<ImportDocumentationCommand, ImportDocumentationResult>
    {
        private readonly IMediator mediator;
        public ImportDocumentationCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ImportDocumentationResult> HandleAsync(ImportDocumentationCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if(!checkImport.IsSuccess)
            {
                return new ImportDocumentationResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            var documentation = request.Documentation.MapToDocumentation();
            documentation.WorkspaceId = request.WorkspaceId;

            if (!string.IsNullOrEmpty(request.Documentation.StructurizrElementId)) { 
                
                var element = await this.mediator.SendQueryAsync<GetElementByStructurizrIdQuery, Element?>(
                    new GetElementByStructurizrIdQuery(
                        request.Documentation.StructurizrElementId,
                        request.WorkspaceId));
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
            await this.mediator.SendCommandAsync(new SaveSingleDocumentationCommand(documentation));

            return new ImportDocumentationResult(request.WorkspaceId, true);
        }
    }
}
