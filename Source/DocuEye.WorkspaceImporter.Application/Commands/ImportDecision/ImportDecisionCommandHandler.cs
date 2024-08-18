using AutoMapper;
using DocuEye.DocsKeeper.Application.Commads.SaveSingleDecision;
using DocuEye.DocsKeeper.Application.Queries.GetDecisonByDslId;
using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDecision
{
    public class ImportDecisionCommandHandler : BaseImportDataCommandHandler, IRequestHandler<ImportDecisionCommand, ImportDecisionResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ImportDecisionCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<ImportDecisionResult> Handle(ImportDecisionCommand request, CancellationToken cancellationToken)
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

            


            var decision = this.mapper.Map<Decision>(request.Decision);
            decision.WorkspaceId = request.WorkspaceId;

            var existingDecision = await this.mediator.Send(
                new GetDecisonByDslIdQuery(
                    decision.WorkspaceId, 
                    decision.DocumentationId, 
                    decision.DslId));
            if(existingDecision != null)
            {
                decision.Id = existingDecision.Id;
            }


            if (!string.IsNullOrEmpty(request.Decision.StrucuturizrElementId))
            {
                var element = await this.mediator.Send(
                    new GetElementByStructurizrIdQuery(
                        request.WorkspaceId,
                        request.Decision.StrucuturizrElementId));
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
            await this.mediator.Send(new SaveSingleDecisionCommand(decision));

            return new ImportDecisionResult(request.WorkspaceId, true);
        }
    }
}
