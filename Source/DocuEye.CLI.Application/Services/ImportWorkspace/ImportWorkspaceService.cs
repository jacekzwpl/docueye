using AutoMapper;
using DocuEye.CLI.ApiClient;
using DocuEye.Structurizr.Json.Model;
using DocuEye.Structurizr.Model;
using DocuEye.Structurizr.Model.Exploders;
using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.WorkspaceImporter.Api.Model.Workspace;
using Microsoft.Extensions.Logging;
using System;
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
            var workspaceData = parameters.WorkspaceData;
            /*
            this.logger.LogInformation("Reading workspace data file");
            if (!File.Exists(parameters.WorkspaceFilePath))
            {
                this.logger.LogError(string.Format("File {0} does not exists.", parameters.WorkspaceFilePath));
                return false;
            }

            var content = File.ReadAllText(parameters.WorkspaceFilePath);

            this.logger.LogInformation("Parsing workspace data file");

            var workspaceData = JsonSerializer.Deserialize<StructurizrJsonWorkspace>(content, this.serializerOptions);

            if (workspaceData == null)
            {
                this.logger.LogError(string.Format("Workspace data parsing failed"));
                return false;
            }*/
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

            workspaceData.Model?.SoftwareSystems?.Where(system => system.Documentation != null).ToList().ForEach(system =>
            {
                var (documentation, decisions, images) = documentationExploder.ExplodeDocumentation(system.Documentation, system.Id);
                documentationsToImport.Add(documentation);
                decisionToImport.AddRange(decisions);
                imageToImport.AddRange(images);

                system.Containers?.Where(container => container.Documentation != null).ToList().ForEach(container => {

                    var (documentation, decisions, images) = documentationExploder.ExplodeDocumentation(container.Documentation, container.Id);
                    documentationsToImport.Add(documentation);
                    decisionToImport.AddRange(decisions);
                    imageToImport.AddRange(images);

                    container.Components?.Where(component => component.Documentation != null).ToList().ForEach(component =>
                    {
                        var (documentation, decisions, images) = documentationExploder.ExplodeDocumentation(component.Documentation, component.Id);
                        documentationsToImport.Add(documentation);
                        decisionToImport.AddRange(decisions);
                        imageToImport.AddRange(images);
                    });

                });
            });

            // Set workspace id if not set
            if (string.IsNullOrEmpty(parameters.WorkspaceId))
            {
                parameters.WorkspaceId = Guid.NewGuid().ToString();
            }
            // Start import
            if (!await this.RunImportStart(parameters, workspaceData))
            {
                return false;
            }

            //Import view configuration
            if(!await this.RunImportViewConfiguration(parameters, viewConfiguration))
            {
                return false;
            }

            //Import elements
            if(!await this.RunImportElements(parameters, elements))
            {
                return false;
            }

            //Import relationships
            if(!await this.RunImportRelationships(parameters, relationships))
            {
                return false;
            }

            //Import views
            if(!await this.RunImportViews(parameters, viewsToImport))
            {
                return false;
            }

            // Clear doc items
            if(!await this.RunImportClearDocItems(parameters))
            {
                return false;
            }

            // Import documentation
            if(!await this.RunImportDocumentation(parameters, documentationsToImport))
            {
                return false;
            }

            // Import images 
            if(!await this.RunImportImage(parameters, imageToImport))
            {
                return false;
            }

            // Import decisions
            if(!await this.RunImportDecisions(parameters, decisionToImport))
            {
                return false;
            }

            // Import decision links
            if(!await this.RunImportDecisionLinks(parameters, decisionToImport))
            {
                return false;
            }

            // Clear decisions
            if(!await this.RunImportClearDecisions(parameters))
            {
                return false;
            }

            // Finish import
            if(!await this.RunImportFinish(parameters))
            {
                return false;
            }

            this.logger.LogInformation("Workspace ID = {0}", parameters.WorkspaceId);
            return true;        
        }

        private async Task<bool> RunImportStart(ImportWorkspaceParameters parameters, StructurizrJsonWorkspace workspaceData)
        {
            string stepName = "Import start";
            this.logger.LogInformation("Running Step: {0}", stepName);
            var result = await this.apiClient.ImportStart(new ImportWorkspaceStartRequest()
            {
                ImportKey = parameters.ImportKey,
                SourceLink = parameters.SourceLink,
                WorkspaceId = parameters.WorkspaceId,
                Visibility = workspaceData.Configuration?.Visibility,
                WorkspaceDescription = workspaceData.Description,
                WorkspaceName = workspaceData.Name,
                AccessRules = this.mapper.Map<IEnumerable<WorkspaceAccessRuleToImport>>(workspaceData.Configuration?.Users)
            });

            this.LogImportStepResult(result, stepName);
            return result.IsSuccess;
        }

        private async Task<bool> RunImportViewConfiguration(ImportWorkspaceParameters parameters, ViewConfigurationToImport viewConfiguration)
        {
            string stepName = "Import view configuration";
            this.logger.LogInformation("Running Step: {0}", stepName);
            var result = await this.apiClient.ImportViewConfiguration(new ImportViewConfigurationRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = parameters.WorkspaceId,
                ViewConfiguration = viewConfiguration
            });

            this.LogImportStepResult(result, stepName);
            return result.IsSuccess;
        }

        private async Task<bool> RunImportElements(ImportWorkspaceParameters parameters, IEnumerable<ElementToImport> elements)
        {
            string stepName = "Import elements";
            this.logger.LogInformation("Running Step: {0}", stepName);
            var result = await this.apiClient.ImportElements(new ImportElementsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = parameters.WorkspaceId,
                Elements = elements
            });
            this.LogImportStepResult(result, stepName);
            return result.IsSuccess;
        }

        private async Task<bool> RunImportRelationships(ImportWorkspaceParameters parameters, IEnumerable<RelationshipToImport> relationships)
        {
            string stepName = "Import elements";
            this.logger.LogInformation("Running Step: {0}", stepName);
            var result = await this.apiClient.ImportRelationships(new ImportRelationshipsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = parameters.WorkspaceId,
                Relationships = relationships
            });
            this.LogImportStepResult(result, stepName);
            return result.IsSuccess;
        }

        private async Task<bool> RunImportViews(ImportWorkspaceParameters parameters, List<ViewToImport> viewsToImport)
        {
            string stepName = "Import views";
            this.logger.LogInformation("Running Step: {0}", stepName);
            var result = await this.apiClient.ImportViews(new ImportViewsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = parameters.WorkspaceId,
                Views = viewsToImport
            });
            this.LogImportStepResult(result, stepName);
            return result.IsSuccess;
        }

        private async Task<bool> RunImportClearDocItems(ImportWorkspaceParameters parameters)
        {
            string stepName = "Clear documentation items";
            this.logger.LogInformation("Running Step: {0}", stepName);
            var result = await this.apiClient.ImportClearDocItems(new ImportClearDocItemsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = parameters.WorkspaceId
            });
            this.LogImportStepResult(result, stepName);
            return result.IsSuccess;
        }

        private async Task<bool> RunImportDocumentation(ImportWorkspaceParameters parameters, List<DocumentationToImport> documentationsToImport)
        {
            string stepName = "Import documentation";
            this.logger.LogInformation("Running Step: {0}", stepName);

            foreach (var documentation in documentationsToImport)
            {
                this.logger.LogInformation("Imort documentation ID = {0}", documentation.Id);
                var result = await this.apiClient.ImportDocumentation(new ImportDocumentationRequest()
                {
                    ImportKey = parameters.ImportKey,
                    WorkspaceId = parameters.WorkspaceId,
                    Documentation = documentation
                });

                
                if (!result.IsSuccess)
                {
                    this.LogImportStepResult(result, stepName);
                    return false;
                }
            }
            this.logger.LogInformation("{0} finished", stepName);
            return true;
        }

        private async Task<bool> RunImportImage(ImportWorkspaceParameters parameters, List<ImageToImport> imagesToImport)
        {
            string stepName = "Import Images";
            this.logger.LogInformation("Running Step: {0}", stepName);
            foreach (var image in imagesToImport)
            {
                var result = await this.apiClient.ImportImage(new ImportImageRequest()
                {
                    ImportKey = parameters.ImportKey,
                    WorkspaceId = parameters.WorkspaceId,
                    Image = image
                });

                
                if (!result.IsSuccess)
                {
                    this.LogImportStepResult(result, stepName);
                    return false;
                }
            }
            this.logger.LogInformation("{0} finished", stepName);
            return true;
        }

        public async Task<bool> RunImportDecisions(ImportWorkspaceParameters parameters, List<DecisionToImport> decisionsToImport)
        {
            string stepName = "Import decisions";
            this.logger.LogInformation("Running Step: {0}", stepName);
            foreach (var decision in decisionsToImport)
            {
                var result = await this.apiClient.ImportDecision(new ImportDecisionRequest()
                {
                    ImportKey = parameters.ImportKey,
                    WorkspaceId = parameters.WorkspaceId,
                    Decision = decision
                });

                
                if (!result.IsSuccess)
                {
                    this.LogImportStepResult(result, stepName);
                    return false;
                }
            }
            this.logger.LogInformation("{0} finished", stepName);
            return true;
        }

        public async Task<bool> RunImportDecisionLinks(ImportWorkspaceParameters parameters, List<DecisionToImport> decisionsToImport)
        {
            string stepName = "Import decisions links";
            this.logger.LogInformation("Running Step: {0}", stepName);
            foreach (var decision in decisionsToImport)
            {
                var result10 = await this.apiClient.ImportDecisionLinks(new ImportDecisionsLinksRequest()
                {
                    ImportKey = parameters.ImportKey,
                    WorkspaceId = parameters.WorkspaceId,
                    DecisionDslId = decision.StrucuturizrId,
                    DocumentationId = decision.DocumentationId,
                    DecisionsLinks = decision.Links ?? Enumerable.Empty<DecisionLinkToImport>()
                });

                this.LogImportStepResult(result10, stepName);
                if (!result10.IsSuccess)
                {
                    return false;
                }
            }
            this.logger.LogInformation("{0} finished", stepName);
            return true;
        }

        private async Task<bool> RunImportClearDecisions(ImportWorkspaceParameters parameters)
        {
            string stepName = "Clear decisions";
            this.logger.LogInformation("Running Step: {0}", stepName);
            var result = await this.apiClient.ImportClearDecisions(new ImportClearDecisionsRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = parameters.WorkspaceId
            });

            this.LogImportStepResult(result, stepName);
            return result.IsSuccess;
        }

        private async Task<bool> RunImportFinish(ImportWorkspaceParameters parameters)
        {
            string stepName = "Import finish";
            this.logger.LogInformation("Running Step: {0}", stepName);
            var result = await this.apiClient.ImportFinish(new ImportFinalizeRequest()
            {
                ImportKey = parameters.ImportKey,
                WorkspaceId = parameters.WorkspaceId
            });

            this.LogImportStepResult(result, stepName);
            return result.IsSuccess;
        }


        private void LogImportStepResult(ImportWorkspaceResponse result, string stepName)
        {
            if (result.IsSuccess)
            {
                this.logger.LogInformation("{0} finished", stepName);
            }
            else
            {
                this.logger.LogInformation("{0} finished with failure.", stepName);
                this.logger.LogInformation("Import result message = {0}", result.Message);
            }
        }
    }
}
