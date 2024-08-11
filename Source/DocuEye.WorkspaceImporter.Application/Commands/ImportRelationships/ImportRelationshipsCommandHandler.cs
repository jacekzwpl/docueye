using AutoMapper;
using DocuEye.ModelKeeper.Application.Commands.SaveRelationships;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.ChangeDetectors;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportRelationships
{
    public class ImportRelationshipsCommandHandler : IRequestHandler<ImportRelationshipsCommand, ImportRelationshipsResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IWorkspaceImporterDBContext dbContext;

        public ImportRelationshipsCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<ImportRelationshipsResult> Handle(ImportRelationshipsCommand request, CancellationToken cancellationToken)
        {
            // Get import data
            var import = await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == request.WorkspaceId && o.Key == request.ImportKey);

            // If no import found then stop
            if (import == null)
            {
                return new ImportRelationshipsResult(
                        request.WorkspaceId,
                        false,
                        string.Format("No import found with key = '{0}'. Start import before continue.",
                            request.ImportKey));
            }
            // if import is already finished then stop
            if (import.EndTime != null)
            {
                return new ImportRelationshipsResult(
                        request.WorkspaceId,
                        false,
                        string.Format("Import with key = '{0}' is already finished.",
                            request.ImportKey));
            }

            //Get existing elements for comparison
            var existingElements = (await this.mediator
                .Send<IEnumerable<Element>>(new GetAllWorkspaceElementsQuery(request.WorkspaceId))).ToList();

            // Get existing relationships for comparison
            var existingRelationships = (await this.mediator
                .Send<IEnumerable<Relationship>>(new GetAllWorkspaceRelationshipsQuery(request.WorkspaceId))).ToList();

            // Detect Changes
            var detector = new RelationshipsChangeDetector(this.mapper, this.mediator);
            var result = await detector.DetectChanges(
                request.WorkspaceId, 
                import, 
                request.Relationships, 
                existingRelationships, 
                existingElements);

            //Save Relationships
            await this.mediator.Send(new SaveRelationshipsCommand()
            {
                RelationshipsToAdd = existingRelationships.Where(o => result.RelationshipsToAdd.Contains(o.Id)).ToArray(),
                RelationshipsToChange = existingRelationships.Where(o => result.RelationshipsToChange.Contains(o.Id)).ToArray(),
                RelationshipsToDelete = existingRelationships.Where(o => result.RelationshipsToAdd.Contains(o.Id)).ToArray()
            });

            return new ImportRelationshipsResult(request.WorkspaceId, true);
        }
    }
}
