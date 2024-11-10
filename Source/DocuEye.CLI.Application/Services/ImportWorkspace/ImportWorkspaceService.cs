using AutoMapper;
using DocuEye.CLI.ApiClient;
using DocuEye.Structurizr.Model;
using DocuEye.Structurizr.Model.Exploders;
using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.WorkspaceImporter.Api.Model.Workspace;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportWorkspace
{
    /// <summary>
    /// Implements logic of workspace import
    /// </summary>
    public class ImportWorkspaceService : IImportWorkspaceService
    {

        private readonly IDocuEyeApiClient apiClient;
        private readonly ILogger<ImportWorkspaceService> logger;
        private JsonSerializerOptions serializerOptions;
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="apiClient">DocuEye api client instance</param>
        /// <param name="logger">ILogger instance</param>
        public ImportWorkspaceService(IDocuEyeApiClient apiClient, IMapper mapper, ILogger<ImportWorkspaceService> logger)
        {
            this.apiClient = apiClient;
            this.mapper = mapper;
            this.logger = logger;
            this.serializerOptions = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <inheritdoc />
        public async Task<bool> Import(ImportWorkspaceParameters parameters)
        {
            this.logger.LogInformation("Reading workspace data file");
            if (!File.Exists(parameters.WorkspaceFilePath))
            {
                this.logger.LogError(string.Format("File {0} does not exists.", parameters.WorkspaceFilePath));
                return false;
            }

            var content = File.ReadAllText(parameters.WorkspaceFilePath);

            this.logger.LogInformation("Parsing workspace data file");

            var workspaceData = JsonSerializer.Deserialize<StructurizrWorkspace>(content, this.serializerOptions);

            if (workspaceData == null)
            {
                this.logger.LogError(string.Format("Workspace data parsing failed"));
                return false;
            }
            this.logger.LogInformation("Detecting workspace contents");
            var modelExploder = new ModelExploder(this.mapper);
            var viewsExploder = new ViewsExploder(this.mapper);
            var documentationExploder = new DocumentationExploder(this.mapper);
            var (elements, relationships) = modelExploder.ExplodeModelElements(workspaceData.Model);
            var viewConfiguration = viewsExploder.ExplodeViewConfiguration(workspaceData.Views?.Configuration);
            var viewsToImport = new List<ViewToImport>();
            viewsToImport.AddRange(viewsExploder.ExplodeSystemLandscapeViews(workspaceData.Views?.SystemLandscapeViews));
            viewsToImport.AddRange(viewsExploder.ExplodeSystemContextViews(workspaceData.Views?.SystemContextViews));
            viewsToImport.AddRange(viewsExploder.ExplodeContainerViews(workspaceData.Views?.ContainerViews));
            viewsToImport.AddRange(viewsExploder.ExplodeComponentViews(workspaceData.Views?.ComponentViews));
            viewsToImport.AddRange(viewsExploder.ExplodeDynamicViews(workspaceData.Views?.DynamicViews));
            viewsToImport.AddRange(viewsExploder.ExplodeFilteredViews(workspaceData.Views?.FilteredViews));
            viewsToImport.AddRange(viewsExploder.ExplodeDeploymentViews(workspaceData.Views?.DeploymentViews));
            viewsToImport.AddRange(viewsExploder.ExplodeImageViews(workspaceData.Views?.ImageViews));


            var documentationsToImport = new List<DocumentationToImport>();
            var decisionToImport = new List<DecisionToImport>();
            var imageToImport = new List<ImageToImport>();
            if (workspaceData.Documentation != null)
            {
                var (documentation, decisions, images) = documentationExploder.ExplodeDocumentation(workspaceData.Documentation);
                documentationsToImport.Add(documentation);
                decisionToImport.AddRange(decisions);
                imageToImport.AddRange(images);
            }

            workspaceData.Model?.SoftwareSystems.Where(system => system.Documentation != null).ToList().ForEach(system =>
            {
                var (documentation, decisions, images) = documentationExploder.ExplodeDocumentation(system.Documentation, system.Id);
                documentationsToImport.Add(documentation);
                decisionToImport.AddRange(decisions);
                imageToImport.AddRange(images);

                system.Containers.Where(container => container.Documentation != null).ToList().ForEach(container => {

                    var (documentation, decisions, images) = documentationExploder.ExplodeDocumentation(container.Documentation, container.Id);
                    documentationsToImport.Add(documentation);
                    decisionToImport.AddRange(decisions);
                    imageToImport.AddRange(images);

                    container.Components.Where(component => component.Documentation != null).ToList().ForEach(component =>
                    {

                        var (documentation, decisions, images) = documentationExploder.ExplodeDocumentation(component.Documentation, component.Id);
                        documentationsToImport.Add(documentation);
                        decisionToImport.AddRange(decisions);
                        imageToImport.AddRange(images);
                    });

                });
            });

            



            this.logger.LogInformation("Importing workspace");
            
            // Start import
            var result = await this.apiClient.ImportStart( new ImportWorkspaceStartRequest()
            {
                ImportKey = parameters.ImportKey,
                SourceLink = parameters.SourceLink,
                WorkspaceId = parameters.WorkspaceId,
                Visibility = workspaceData.Configuration?.Visibility,
                AccessRules = this.mapper.Map<IEnumerable<WorkspaceAccessRuleToImport>>(workspaceData.Configuration?.Users)
            });

            //set workspaceId
            var workspaceId = result.WorkspaceId;

            //Import view configuration
            var result2 = await this.apiClient.ImportViewConfiguration(new ImportViewConfigurationRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = workspaceId,
                ViewConfiguration = viewConfiguration
            });

            //Import elements
            var resutlt3 = await this.apiClient.ImportElements(new ImportElementsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = workspaceId,
                Elements = elements
            });

            //Import relationships
            var result4 = await this.apiClient.ImportRelationships(new ImportRelationshipsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = workspaceId,
                Relationships = relationships
            });

            //Import views
            var result5 = await this.apiClient.ImportViews(new ImportViewsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = workspaceId,
                Views = viewsToImport
            });

            // Clear doc items
            var result6 = await this.apiClient.ImportClearDocItems(new ImportClearDocItemsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = workspaceId
            });

            // Import documentation
            foreach (var documentation in documentationsToImport)
            {
                var result7 = await this.apiClient.ImportDocumentation(new ImportDocumentationRequest()
                {
                    ImportKey = parameters.ImportKey,
                    WorkspaceId = workspaceId,
                    Documentation = documentation
                });
            }

            // Import images 
            foreach (var image in imageToImport)
            {
                var result8 = await this.apiClient.ImportImage(new ImportImageRequest()
                {
                    ImportKey = parameters.ImportKey,
                    WorkspaceId = workspaceId,
                    Image = image
                });
            }

            // Import decisions
            foreach (var decision in decisionToImport)
            {
                var result9 = await this.apiClient.ImportDecision(new ImportDecisionRequest()
                {
                    ImportKey = parameters.ImportKey,
                    WorkspaceId = workspaceId,
                    Decision = decision
                });
            }

            // Import decision links
            foreach (var decision in decisionToImport)
            {
                var result10 = await this.apiClient.ImportDecisionLinks(new ImportDecisionsLinksRequest()
                {
                    ImportKey = parameters.ImportKey,
                    WorkspaceId = workspaceId,
                    DecisionDslId = decision.StrucuturizrId,
                    DocumentationId = decision.DocumentationId,
                    DecisionsLinks = decision.Links ?? Enumerable.Empty<DecisionLinkToImport>()
                });
            }

            // Clear decisions
            var result11 = await this.apiClient.ImportClearDecisions(new ImportClearDecisionsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = workspaceId
            });

            // Finish import
            var result12 = await this.apiClient.ImportFinish(new ImportFinalizeRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = workspaceId
            });


            /*



            var result2 = await this.apiClient.ImportViewConfiguration(new ImportViewConfigurationRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = workspaceId,
                ViewConfiguration = workspaceData.ViewConfiguration
            });

            */


            if (result.IsSuccess)
            {
                this.logger.LogInformation("Import finished with success");
                this.logger.LogInformation("Workspace ID = {0}", result.WorkspaceId);

            }
            else
            {
                this.logger.LogInformation("Import finished with failure.");
                this.logger.LogInformation("Import result message = {0}", result.Message);
            }


            return result.IsSuccess;
        }

        


        /// <inheritdoc />
        public async Task<bool> ImportOLD(ImportWorkspaceParameters parameters)
        {
            this.logger.LogInformation("Reading workspace data file");
            if (!File.Exists(parameters.WorkspaceFilePath))
            {
                this.logger.LogError(string.Format("File {0} does not exists.", parameters.WorkspaceFilePath));
                return false;
            }

            var content = File.ReadAllText(parameters.WorkspaceFilePath);

            this.logger.LogInformation("Parsing workspace data file");

            var workspaceData = JsonSerializer.Deserialize<StructurizrWorkspace>(content, this.serializerOptions);

            if(workspaceData == null)
            {
                this.logger.LogError(string.Format("Workspace data parsing failed"));
                return false;
            }
            this.logger.LogInformation("Importing workspace");
            var result = await this.apiClient.ImportWorkspace(new ImportWorkspaceRequest() { 
                ImportKey = parameters.ImportKey,
                SourceLink = parameters.SourceLink,
                WorkspaceId = parameters.WorkspaceId,
                WorkspaceData = workspaceData
            });

            
            if(result.IsSuccess)
            {
                this.logger.LogInformation("Import finished with success");
                this.logger.LogInformation("Workspace ID = {0}", result.WorkspaceId);
                
            }else
            {
                this.logger.LogInformation("Import finished with failure.");
                this.logger.LogInformation("Import result message = {0}", result.Message);
            }
            
           
            return result.IsSuccess;
        }
    }
}
