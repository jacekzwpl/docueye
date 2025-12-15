using DocuEye.DocsKeeper.Application.Commads.SaveSingleDecision;
using DocuEye.DocsKeeper.Application.Model;
using DocuEye.DocsKeeper.Application.Queries.GetDecisionsList;
using DocuEye.DocsKeeper.Application.Queries.GetDecisonByDslId;
using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.Maps;
using DocuEye.WorkspaceImporter.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDecisionsLinks
{
    public class ImportDecisionsLinksCommandHandler : BaseImportDataCommandHandler, ICommandHandler<ImportDecisionsLinksCommand, ImportDecisionsLinksResult>
    {
        private readonly IMediator mediator;

        public ImportDecisionsLinksCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ImportDecisionsLinksResult> HandleAsync(ImportDecisionsLinksCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ImportDecisionsLinksResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            var decision = await this.mediator.SendQueryAsync<GetDecisonByDslIdQuery, Decision?>(
                new GetDecisonByDslIdQuery(
                    request.WorkspaceId, request.DocumentationId, request.DecisionDslId));
            if(decision == null)
            {
                return new ImportDecisionsLinksResult(
                        request.WorkspaceId,
                        false,
                        string.Format("Decison with dslid = '{0}' not found",request.DecisionDslId));
            }

            var allDecsions = await this.mediator.SendQueryAsync<GetDecisionsListQuery, IEnumerable<DecisionsListItem>>(
                new GetDecisionsListQuery(request.WorkspaceId));

            var decisionLinks = new List<DecisionLink>();

            foreach (var link in request.DecisionLinks)
            {
                var linkedDecision = allDecsions
                    .Where(o => 
                        o.DslId == link.StructurizrId 
                        && o.DocumentationId == request.DocumentationId)
                    .SingleOrDefault();
                
                if (linkedDecision != null)
                {
                    var decisionLink = link.MapToDecisionLink();
                    decisionLink.Id = linkedDecision.Id;
                    decisionLinks.Add(decisionLink);
                }
            }
            decision.Links = decisionLinks;

            await this.mediator.SendCommandAsync(new SaveSingleDecisionCommand(decision));



            return new ImportDecisionsLinksResult(request.WorkspaceId, true);
        }
    }
}
