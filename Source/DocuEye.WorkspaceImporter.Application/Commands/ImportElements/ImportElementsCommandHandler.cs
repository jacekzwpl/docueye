using DocuEye.ModelKeeper.Application.Commands.SaveElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
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
    public class ImportElementsCommandHandler : BaseImportDataCommandHandler, IRequestHandler<ImportElementsCommand, ImportElementsResult>
    {
        private readonly IMediator mediator;

        public ImportElementsCommandHandler(IMediator mediator, IWorkspaceImporterDBContext dbContext) : base(dbContext)
        {
            this.mediator = mediator;
        }

        public async Task<ImportElementsResult> Handle(ImportElementsCommand request, CancellationToken cancellationToken)
        {

            // Check import data
            var import = await this.GetImport(request.WorkspaceId, request.ImportKey);
            var checkImport = this.CheckImport(import, request.WorkspaceId, request.ImportKey);
            if (!checkImport.IsSuccess)
            {
                return new ImportElementsResult(
                        request.WorkspaceId,
                        false,
                        checkImport.Message);
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
            var detector = new ElementsChangeDetector(this.mediator);
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
