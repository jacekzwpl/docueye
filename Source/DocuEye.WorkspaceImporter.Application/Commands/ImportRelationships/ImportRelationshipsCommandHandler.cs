using AutoMapper;
using DocuEye.ModelKeeper.Application.Commands.SaveRelationships;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.ChangeDetectors;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportRelationships
{
    public class ImportRelationshipsCommandHandler : BaseImportDataCommandHandler, IRequestHandler<ImportRelationshipsCommand, ImportRelationshipsResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ImportRelationshipsCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<ImportRelationshipsResult> Handle(ImportRelationshipsCommand request, CancellationToken cancellationToken)
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
