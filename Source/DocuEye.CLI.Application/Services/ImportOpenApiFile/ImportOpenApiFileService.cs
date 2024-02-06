using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using DocuEye.Structurizr.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportOpenApiFile
{
    public class ImportOpenApiFileService : IImportOpenApiFileService
    {
        private readonly IDocuEyeApiClient apiClient;
        private readonly ILogger<ImportWorkspaceService> logger;
        private JsonSerializerOptions serializerOptions;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="apiClient">DocuEye api client instance</param>
        /// <param name="logger">ILogger instance</param>
        public ImportOpenApiFileService(IDocuEyeApiClient apiClient, ILogger<ImportWorkspaceService> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
            this.serializerOptions = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<bool> Import(ImportOpenApiFileParameters parameters)
        {
            this.logger.LogInformation("Reading open api data file");
            if (!File.Exists(parameters.OpenApiFile))
            {
                this.logger.LogError(string.Format("File {0} does not exists.", parameters.OpenApiFile));
                return false;
            }
            var bytes = File.ReadAllBytes(parameters.OpenApiFile);
            var content = Convert.ToBase64String(bytes);
            
            this.logger.LogInformation("Importing openapi");
            var result = await this.apiClient.ImportOpenApiFile(parameters.WorkspaceId, parameters.ElementId, new ImportOpenApiFileRequest()
            {
                Content = content,
                Name = Path.GetFileName(parameters.OpenApiFile)
            });


            if (string.IsNullOrEmpty(result))
            {
                this.logger.LogInformation("Import finished with success");
                return true;
            }
            else
            {
                this.logger.LogInformation("Import finished with failure.");
                this.logger.LogInformation("Import result message = {0}", result);
                return false;
            }

        }
    }
}
