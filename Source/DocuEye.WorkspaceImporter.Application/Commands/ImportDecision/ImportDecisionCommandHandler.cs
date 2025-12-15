using DocuEye.DocsKeeper.Application.Commads.SaveSingleDecision;
using DocuEye.DocsKeeper.Application.Queries.GetDecisonByDslId;
using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Maps;
using DocuEye.WorkspaceImporter.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDecision
{
    public class ImportDecisionCommandHandler : BaseImportDataCommandHandler, ICommandHandler<ImportDecisionCommand, ImportDecisionResult>
    {
        private readonly IMediator mediator;

        public ImportDecisionCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ImportDecisionResult> HandleAsync(ImportDecisionCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ImportDecisionResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }




            var decision = request.Decision.MapToDecision();
            decision.WorkspaceId = request.WorkspaceId;

            var existingDecision = await this.mediator.SendQueryAsync<GetDecisonByDslIdQuery, Decision?>(
                new GetDecisonByDslIdQuery(
                    decision.WorkspaceId, 
                    decision.DocumentationId, 
                    decision.DslId));
            if(existingDecision != null)
            {
                decision.Id = existingDecision.Id;
            }else
            {
                decision.Id = Guid.NewGuid().ToString();
            }

            decision.ImportKey = request.ImportKey;


            if (!string.IsNullOrEmpty(request.Decision.StrucuturizrElementId))
            {
                var element = await this.mediator.SendQueryAsync<GetElementByStructurizrIdQuery, Element?>(
                    new GetElementByStructurizrIdQuery(
                        request.Decision.StrucuturizrElementId,
                        request.WorkspaceId));
                if (element != null)
                {
                    decision.ElementId = element.Id;
                }
                else
                {
                    return new ImportDecisionResult(
                        request.WorkspaceId,
                        false,
                        string.Format("Element with structurizr id = '{0}' not found in workspace", request.Decision.StrucuturizrElementId));
                }
            }

            // save decision
            await this.mediator.SendCommandAsync(new SaveSingleDecisionCommand(decision));

            return new ImportDecisionResult(request.WorkspaceId, true);
        }
    }
}
