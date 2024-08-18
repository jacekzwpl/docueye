using AutoMapper;
using AutoMapper.Configuration.Annotations;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Application.Commands.SaveViewsChanges;
using DocuEye.ViewsKeeper.Application.Queries.GetAllViews;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Application.ChangeDetectors;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace;
using DocuEye.WorkspacesKeeper.Application.Queries.GetWorkspace;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportViews
{
    public class ImportViewsCommandHandler : IRequestHandler<ImportViewsCommand, ImportViewsResult>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IWorkspaceImporterDBContext dbContext;

        public ImportViewsCommandHandler(IMediator mediator, IMapper mapper, IWorkspaceImporterDBContext dbContext)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<ImportViewsResult> Handle(ImportViewsCommand request, CancellationToken cancellationToken)
        {
            // Get import data
            var import = await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == request.WorkspaceId && o.Key == request.ImportKey);

            // If no import found then stop
            if (import == null)
            {
                return new ImportViewsResult(
                        request.WorkspaceId,
                        false,
                        string.Format("No import found with key = '{0}'. Start import before continue.",
                            request.ImportKey));
            }
            // if import is already finished then stop
            if (import.EndTime != null)
            {
                return new ImportViewsResult(
                        request.WorkspaceId,
                        false,
                string.Format("Import with key = '{0}' is already finished.",
                            request.ImportKey));
            }

            //Get workspace for saving views information
            var workspace = await this.mediator.Send(
                new GetWorkspaceQuery(request.WorkspaceId));

            if(workspace == null)
            {
                return new ImportViewsResult(
                        request.WorkspaceId,
                        false,
                string.Format("Workspace with ID = '{0}' not exists.",
                            request.WorkspaceId));
            }

            var (
                systemLandscapeViews,
                systemContextViews,
                containerViews,
                componentViews,
                dynamicViews,
                deploymentViews,
                imagesViews,
                filteredViews,
                worskspaceViews) = await this.DetectChanges(request, import);

            workspace.Views = worskspaceViews;

            //Save views
            await this.mediator.Send(new SaveViewsChangesCommand(request.WorkspaceId)
            {
                ComponentViews = componentViews,
                ContainerViews = containerViews,
                DeploymentViews = deploymentViews,
                DynamicViews = dynamicViews,
                FilteredViews = filteredViews,
                ImagesViews = imagesViews,
                SystemContextViews = systemContextViews,
                SystemLandscapeViews = systemLandscapeViews
            });

            //Save Workspace
            await this.mediator.Send(new SaveWorkspaceCommand()
            {
                Workspace = workspace
            });

            return new ImportViewsResult(request.WorkspaceId, true);


        }


        private async Task<(
            IEnumerable<SystemLandscapeView>,
            IEnumerable<SystemContextView>,
            IEnumerable<ContainerView>,
            IEnumerable<ComponentView>,
            IEnumerable<DynamicView>,
            IEnumerable<DeploymentView>,
            IEnumerable<ImageView>,
            IEnumerable<FilteredView>,
            IEnumerable<WorkspaceView>
            )> DetectChanges(ImportViewsCommand request, WorkspaceImport import)
        {
            //Get existing elements for comparison
            var existingElements = await this.mediator
                .Send<IEnumerable<Element>>(new GetAllWorkspaceElementsQuery(request.WorkspaceId));
            //Get existing relationships for comparison
            var existingRelationships = await this.mediator
                .Send<IEnumerable<Relationship>>(new GetAllWorkspaceRelationshipsQuery(request.WorkspaceId));
            // Get exiting views for comparision
            var existingViews = await this.mediator
                .Send<IEnumerable<BaseView>>(new GetAllViewsQuery(request.WorkspaceId));

            var detector = new ViewsChangeDetector(this.mapper, this.mediator);
            var(viewsIdsMap, elementsDiagrams) = detector.DetectViewsIds(
                request.Views, existingViews, existingElements);

            var systemLandscapeViews = detector.DetectSystemLandscapeViews(
                request.WorkspaceId,
                request.Views,
                viewsIdsMap,
                existingElements, 
                existingRelationships, 
                elementsDiagrams);

            var systemContextViews = detector.DetectSystemContextViews(
                request.WorkspaceId, 
                request.Views, 
                viewsIdsMap, 
                existingElements, 
                existingRelationships, 
                elementsDiagrams);

            var containerViews = detector.DetectContainerViews(
                request.WorkspaceId,
                request.Views,
                viewsIdsMap,
                existingElements,
                existingRelationships,
                elementsDiagrams);

            var componentViews = detector.DetectComponentViews(
                request.WorkspaceId,
                request.Views,
                viewsIdsMap,
                existingElements,
                existingRelationships,
                elementsDiagrams);

            var dynamicViews = detector.DetectDynamicViews(
                request.WorkspaceId,
                request.Views,
                viewsIdsMap,
                existingElements,
                existingRelationships,
                elementsDiagrams);

            var deploymentViews = detector.DetectDeploymentViews(
                request.WorkspaceId,
                request.Views,
                viewsIdsMap,
                existingElements,
                existingRelationships,
                elementsDiagrams);

            var imagesViews = detector.DetectImageViews(
                request.WorkspaceId, 
                request.Views, 
                viewsIdsMap, 
                existingElements);

            var filteredViews = detector.DetectFilteredViews(
                request.WorkspaceId,
                request.Views,
                viewsIdsMap, 
                systemLandscapeViews, 
                systemContextViews, 
                containerViews, 
                componentViews);

            var workspaceViews = new List<WorkspaceView>();
            workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(systemLandscapeViews));
            workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(systemContextViews));
            workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(containerViews));
            workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(componentViews));
            workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(dynamicViews));
            workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(deploymentViews));
            workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(imagesViews));
            workspaceViews.AddRange(this.mapper.Map<IEnumerable<WorkspaceView>>(filteredViews));

            
            return (
                systemLandscapeViews, 
                systemContextViews, 
                containerViews, 
                componentViews, 
                dynamicViews, 
                deploymentViews, 
                imagesViews, 
                filteredViews,
                workspaceViews);

        } 

       

    }
}
