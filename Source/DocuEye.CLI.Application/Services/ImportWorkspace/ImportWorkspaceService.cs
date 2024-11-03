using AutoMapper;
using DocuEye.CLI.ApiClient;
using DocuEye.Structurizr.Model;
using DocuEye.Structurizr.Model.Exploders;
using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
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
            var (elements, relationships) = modelExploder.ExplodeModelElements(workspaceData.Model);
            



            this.logger.LogInformation("Importing workspace");
            
            // Start import
            var result = await this.apiClient.ImportStart( new ImportWorkspaceStartRequest()
            {
                ImportKey = parameters.ImportKey,
                SourceLink = parameters.SourceLink,
                WorkspaceId = parameters.WorkspaceId
            });

            /*

            var workspaceId = result.WorkspaceId;

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
