using AutoMapper;
using DocuEye.DocsKeeper.Application.Commads.SaveDecisions;
using DocuEye.DocsKeeper.Application.Commads.SaveDocumentationChanges;
using DocuEye.DocsKeeper.Application.Commads.SaveImages;
using DocuEye.DocsKeeper.Application.Queries.GetDecisionsList;
using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Application.Commands.SaveElements;
using DocuEye.ModelKeeper.Application.Commands.SaveRelationships;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements;
using DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceRelationships;
using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Application.Commands.SaveViewsChanges;
using DocuEye.ViewsKeeper.Application.Queries.GetAllViews;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.ViewConfigurationChangeDetector;
using DocuEye.WorkspaceImporter.Application.Services.ViewsExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveViewConfiguration;
using DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportWorkspace
{
    /// <summary>
    /// Handler for ImportWorkspaceCommand
    /// </summary>
    public class ImportWorkspaceCommandHandler : IRequestHandler<ImportWorkspaceCommand, ImportWorkspaceResult>
    {
        private readonly IWorkspaceChangeDetectorService detector;
        private readonly IViewConfigurationChangeDetector viewConfigurationDetector;
        private readonly IModelExploderService modelExploder;
        private readonly IViewsExploderService viewsExploder;
        private readonly IDecisionsExploderService decisionsExloder;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IWorkspaceImporterDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="detector">Workspace change detector service</param>
        /// <param name="viewConfigurationDetector">View configuration change detector service</param>
        /// <param name="modelExploder">Model exploder service</param>
        /// <param name="viewsExploder">Views exploder service</param>
        /// <param name="decisionsExloder">Decisions exploder service</param>
        /// <param name="mapper">Automapper service</param>
        /// <param name="mediator">Mediator service</param>
        /// <param name="dbContext">MongoDB context</param>
        public ImportWorkspaceCommandHandler(
            IWorkspaceChangeDetectorService detector, 
            IViewConfigurationChangeDetector viewConfigurationDetector, 
            IModelExploderService modelExploder, 
            IViewsExploderService viewsExploder, 
            IDecisionsExploderService decisionsExloder, 
            IMapper mapper, 
            IMediator mediator,
            IWorkspaceImporterDBContext dbContext)
        {
            this.detector = detector;
            this.viewConfigurationDetector = viewConfigurationDetector;
            this.modelExploder = modelExploder;
            this.viewsExploder = viewsExploder;
            this.decisionsExloder = decisionsExloder;
            this.mapper = mapper;
            this.mediator = mediator;
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles command
        /// </summary>
        /// <param name="request">Command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task<ImportWorkspaceResult> Handle(ImportWorkspaceCommand request, CancellationToken cancellationToken)
        {
            //Create workspace object
            var workspace = new Workspace()
            {
                Id = string.IsNullOrEmpty(request.WorkspaceId) ? Guid.NewGuid().ToString() : request.WorkspaceId,
                Name = request.WorkspaceData.Name ?? string.Empty,
                Description = request.WorkspaceData.Description,
                IsPrivate = request.WorkspaceData.Configuration?.Visibility.ToLower() == "private"
                    ? true : false,
                AccessRules = request.WorkspaceData.Configuration?.Users != null
                    ? this.mapper.Map<IEnumerable<WorkspaceAccessRule>>(request.WorkspaceData.Configuration?.Users)
                    : Enumerable.Empty<WorkspaceAccessRule>()
            };

            //Create import object
            var import = new WorkspaceImport()
            {
                Id = Guid.NewGuid().ToString(),
                Key = request.ImportKey,
                SourceLink = request.SourceLink,
                WorkspaceId = workspace.Id,
                StartTime = DateTime.UtcNow,
            };

            // Get currently running import if any
            var runningImport = await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == workspace.Id && o.EndTime == null);
            if(runningImport != null)
            {
                //If currently running import has different key then stop
                if(runningImport.Key != import.Key)
                {
                    return new ImportWorkspaceResult(
                        workspace.Id, 
                        false, 
                        string.Format("Can not start import because another import with key = '{0}' is currently running.", 
                            runningImport.Key));
                }
                //If key is the same means that for some reason import import did not finish and user wants to repeat action.
            }else
            {
                //if import with the same key already finished stop action
                var existingImport = await this.dbContext.WorkspaceImports
                .FindOne(o => o.WorkspaceId == workspace.Id && o.Key == import.Key);
                if(existingImport != null)
                {
                    return new ImportWorkspaceResult(
                        workspace.Id,
                        false,
                        string.Format("Can not start import because another import with key = '{0}' is already completed.",
                            import.Key));
                }
                
                await this.dbContext.WorkspaceImports.InsertOneAsync(import);
            }
            

            //Get existing elements for comparison
            var existingElements = await this.mediator
                .Send<IEnumerable<Element>>(new GetAllWorkspaceElementsQuery(workspace.Id));
            //Get existing relationships for comparison
            var existingRelationships = await this.mediator
                .Send<IEnumerable<Relationship>>(new GetAllWorkspaceRelationshipsQuery(workspace.Id));
            // Get exiting views for comparision
            var existingViews = await this.mediator
                .Send<IEnumerable<BaseView>>(new GetAllViewsQuery(workspace.Id));
            // Get Existing decisions for comparision
            var existingDecisions = await this.mediator
                .Send<IEnumerable<DecisionsListItem>>(new GetDecisionsListQuery(workspace.Id));

            //Explode data in structurizr format
            var explodeModelResult = this.modelExploder.ExplodeModel(request.WorkspaceData.Model);
            var explodeViewsResult = this.viewsExploder.Explode(request.WorkspaceData.Views);
            var explodedDecisions = this.decisionsExloder.Explode(request.WorkspaceData.Documentation);

            // Detect changes in model data
            var elementsChangesResult = await this.detector.DetectElementsChanges(workspace.Id, import, explodeModelResult.Elements, existingElements, existingDecisions);
            var relationshipsChnagesResult = await this.detector.DetectRelaionshipsChanges(workspace.Id, import, explodeModelResult.Relationships, existingRelationships, explodeModelResult.Elements);
            //Detect changes in additional data
            var viewChnagesResult = this.detector.DetectViewsChanges(workspace.Id, explodeViewsResult, explodeModelResult.Elements, explodeModelResult.Relationships, existingViews);

            var workspaceDocumentation = this.detector.DetectDocumentationChanges(workspace.Id, request.WorkspaceData.Documentation);
            var workspaceDecisions = this.detector.DetectDecisionChnages(workspace.Id, workspaceDocumentation.Id, null, explodedDecisions, existingDecisions);
            var workspaceDocumentationImages = this.detector.DetectImagesChanges(workspace.Id, workspaceDocumentation.Id, request.WorkspaceData.Documentation?.Images);
            var viewConfiguration = this.viewConfigurationDetector.DetectViewConfigurationChanges(workspace.Id, request.WorkspaceData);

            //Apply documentation changes for saving
            List<Documentation> documentationsToAdd = new List<Documentation>();
            List<Decision> decisionsToAdd = new List<Decision>();
            List<Image> imagesToAdd = new List<Image>();
            documentationsToAdd.Add(workspaceDocumentation);
            decisionsToAdd.AddRange(workspaceDecisions);
            imagesToAdd.AddRange(workspaceDocumentationImages);
            documentationsToAdd.AddRange(elementsChangesResult.DocumentationsToAdd);
            imagesToAdd.AddRange(elementsChangesResult.ImagesToAdd);
            decisionsToAdd.AddRange(elementsChangesResult.DecisionsToAdd);

            //Apply views information to workspace
            workspace.Views = viewChnagesResult.WorkspaceViews;

            // Save Workspace ViewConfiguration
            await this.mediator.Send(new SaveViewConfigurationCommand()
            {
                ViewConfiguration = viewConfiguration
            });

            //Save Elements
            await this.mediator.Send(new SaveElementsCommand() { 
                ElementsToAdd = elementsChangesResult.ElementsToAdd,
                ElementsToChange = elementsChangesResult.ElementsToChange,
                ElementsToDelete = elementsChangesResult.ElementsToDelete
            });

            //Save Relationships
            await this.mediator.Send(new SaveRelationshipsCommand()
            {
                RelationshipsToAdd = relationshipsChnagesResult.RelationshipsToAdd,
                RelationshipsToChange = relationshipsChnagesResult.RelationshipsToChange,
                RelationshipsToDelete = relationshipsChnagesResult.RelationshipsToDelete
            });

            //Save views
            await this.mediator.Send(new SaveViewsChangesCommand(workspace.Id) { 
                ComponentViews = viewChnagesResult.ComponentViewsToAdd,
                ContainerViews = viewChnagesResult.ContainerViewsToAdd,
                DeploymentViews = viewChnagesResult.DeploymentViewsToAdd,
                DynamicViews = viewChnagesResult.DynamicViewsToAdd,
                FilteredViews = viewChnagesResult.FilteredViewsToAdd,
                ImagesViews = viewChnagesResult.ImagesViewsToAdd,
                SystemContextViews = viewChnagesResult.SystemContextViewsToAdd,
                SystemLandscapeViews = viewChnagesResult.SystemLandscapeViewsToAdd
            });

            //Save documentations
            await this.mediator.Send(new SaveDocumentationCommand(workspace.Id)
            {
                DocumentationsToAdd = documentationsToAdd,
            });
            //Save decisions
            await this.mediator.Send(new SaveDecisionsCommand(workspace.Id)
            {
                DecisionsToAdd = decisionsToAdd,
            });
            //Save images
            await this.mediator.Send(new SaveImagesCommand(workspace.Id)
            {
                ImagesToAdd = imagesToAdd,
            });

            //Save Workspace
            await this.mediator.Send(new SaveWorkspaceCommand()
            {
                Workspace = workspace
            });

            //Finish import
            import.EndTime = DateTime.UtcNow;
            await this.dbContext.WorkspaceImports.ReplaceOneAsync(import);

            return new ImportWorkspaceResult(workspace.Id, true);
        }
    }
}
