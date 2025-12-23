using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.ModelKeeper.Application.Commands.SaveRelationships;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.ChangeDetectors;
using DocuEye.WorkspaceImporter.Persistence;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportRelationships
{
    public class ImportRelationshipsCommandHandler : BaseImportDataCommandHandler, ICommandHandler<ImportRelationshipsCommand, ImportRelationshipsResult>
    {
        private readonly IMediator mediator;

        public ImportRelationshipsCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ImportRelationshipsResult> HandleAsync(ImportRelationshipsCommand request, CancellationToken cancellationToken)
        {
            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ImportRelationshipsResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
            }

            //Get existing elements for comparison
            var existingElements = (await this.mediator
                .SendQueryAsync<GetAllWorkspaceElementsQuery,IEnumerable<Element>>(new GetAllWorkspaceElementsQuery(request.WorkspaceId))).ToList();

            // Get existing relationships for comparison
            var existingRelationships = (await this.mediator
                .SendQueryAsync<GetAllWorkspaceRelationshipsQuery,IEnumerable<Relationship>>(new GetAllWorkspaceRelationshipsQuery(request.WorkspaceId))).ToList();
            var oldRelationships = new List<Relationship>(existingRelationships);
            // Detect Changes
            var detector = new RelationshipsChangeDetector(this.mediator);
            var result = await detector.DetectChanges(
                request.WorkspaceId, 
                import, 
                request.Relationships, 
                existingRelationships, 
                existingElements);
            
            //Save Relationships
            await this.mediator.SendCommandAsync(new SaveRelationshipsCommand()
            {
                RelationshipsToAdd = existingRelationships.Where(o => result.RelationshipsToAdd.Contains(o.Id)).ToArray(),
                RelationshipsToChange = existingRelationships.Where(o => result.RelationshipsToChange.Contains(o.Id)).ToArray(),
                RelationshipsToDelete = oldRelationships.Where(o => result.RelationshipsToDelete.Contains(o.Id)).ToArray()
            });

            return new ImportRelationshipsResult(request.WorkspaceId, true);
        }
    }
}
