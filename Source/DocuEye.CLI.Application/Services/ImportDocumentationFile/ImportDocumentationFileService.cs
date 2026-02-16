using DocuEye.CLI.ApiClient;
using DocuEye.WorkspaceImporter.Api.Model;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportDocumentationFile
{
    public class ImportDocumentationFileService : IImportDocumentationFileService
    {
        private readonly IDocuEyeApiClient apiClient;
        private readonly ILogger<ImportDocumentationFileService> logger;


        public ImportDocumentationFileService(IDocuEyeApiClient apiClient, ILogger<ImportDocumentationFileService> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
        }

        public async Task<bool> Import(ImportDocumentationFileParameters parameters)
        {
            this.logger.LogInformation("Reading documentation file");
            if (!File.Exists(parameters.DocumentationFile))
            {
                this.logger.LogError(string.Format("File {0} does not exists.", parameters.DocumentationFile));
                return false;
            }
            var bytes = File.ReadAllBytes(parameters.DocumentationFile);
            var content = Convert.ToBase64String(bytes);

            this.logger.LogInformation("Importing documentation file");
            var result = await this.apiClient.ImportDocumentationFile(parameters.WorkspaceId, new ImportDocumentationFileRequest()
            {
                Content = content,
                Name = Path.GetFileName(parameters.DocumentationFile),
                ElementDslId = parameters.ElementDslId,
                ElementId = parameters.ElementId,
                DocumentationType = parameters.DocumentationFileType
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
