using AutoMapper;
using DocuEye.ModelKeeper.Application.Commands.SaveElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Application.ChangeDetectors;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspaceImporter.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportElements
{
    public class ImportElementsCommandHandler : IRequestHandler<ImportElementsCommand, ImportElementsResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IWorkspaceImporterDBContext dbContext;

        public ImportElementsCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<ImportElementsResult> Handle(ImportElementsCommand request, CancellationToken cancellationToken)
        {

            // Get import data
            var import = await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == request.WorkspaceId && o.Key == request.ImportKey);

            // If no import found then stop
            if (import == null)
            {
                return new ImportElementsResult(
                        request.WorkspaceId,
                        false,
                        string.Format("No import found with key = '{0}'. Start import before continue.",
                            request.ImportKey));
            }
            // if import is already finished then stop
            if(import.EndTime != null)
            {
                return new ImportElementsResult(
                        request.WorkspaceId,
                        false,
                        string.Format("Import with key = '{0}' is already finished.",
                            request.ImportKey));
            }

            //Get existing elements for comparison
            var existingElements = (await this.mediator
                .Send<IEnumerable<Element>>(new GetAllWorkspaceElementsQuery(request.WorkspaceId))).ToList();

            // Detect Changes
            var result = await this.DetectChanges(request.WorkspaceId, import, request.Elements, existingElements);

            //Save Elements
            await this.mediator.Send(new SaveElementsCommand()
            {
                ElementsToAdd = existingElements.Where(o => result.ElementsToAdd.Contains(o.Id)).ToArray(),
                ElementsToChange = existingElements.Where(o => result.ElementsToChange.Contains(o.Id)).ToArray(),
                ElementsToDelete = existingElements.Where(o => result.ElementsToDelete.Contains(o.Id)).ToArray()
            });

            return new ImportElementsResult(request.WorkspaceId, true);
        }

        private async Task<ElementsChangeDetectorResult> DetectChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
        {
            var result = new ElementsChangeDetectorResult();
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);
            var softwareSystemsResult = await detector.DetectSoftwareSystemsChanges(workspaceId, import, elementsToImport, existingElements);
            result.AddResult(softwareSystemsResult);

            var containersResult = await detector.DetectContainersChanges(workspaceId, import, elementsToImport, existingElements);
            result.AddResult(containersResult);

            var componentsResult = await detector.DetectComponentsChanges(workspaceId, import, elementsToImport, existingElements);
            result.AddResult(componentsResult);

            var deploymentNodesResult = await detector.DetectDeploymentNodesChanges(workspaceId, import, elementsToImport, existingElements);
            result.AddResult(deploymentNodesResult);

            var infrastructureNodesResult = await detector.DetectInfrastructureNodesChanges(workspaceId, import, elementsToImport, existingElements);
            result.AddResult(infrastructureNodesResult);

            var peopleResult = await detector.DetectPeopleChanges(workspaceId, import, elementsToImport, existingElements);
            result.AddResult(peopleResult);

            var softwareSystemInstancesResult = await detector.DetectSoftwareSystemInstancesChanges(workspaceId, import, elementsToImport, existingElements);
            result.AddResult(softwareSystemInstancesResult);

            var containerInstancesResult = await detector.DetectContainerInstancesChanges(workspaceId, import, elementsToImport, existingElements);
            result.AddResult(containerInstancesResult);

            return result;
        }
    }
}
