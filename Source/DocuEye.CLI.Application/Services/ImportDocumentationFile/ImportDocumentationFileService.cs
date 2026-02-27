using DocuEye.CLI.ApiClient;
using DocuEye.WorkspaceImporter.Api.Model;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportDocumentationFile
{
    public class ImportDocumentationFileService : IImportDocumentationFileService
    {
        private readonly IDocuEyeApiClient apiClient;
        private readonly ILogger<ImportDocumentationFileService> logger;
        private HttpClient httpClient;

        public ImportDocumentationFileService(IDocuEyeApiClient apiClient, ILogger<ImportDocumentationFileService> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
            this.httpClient = new HttpClient();
        }

        public async Task<bool> Import(ImportDocumentationFileParameters parameters)
        {
            this.logger.LogInformation("Reading documentation file");
            string content = string.Empty;
            Uri? validatedUri;
            var isValid = Uri.TryCreate(parameters.DocumentationFile, UriKind.Absolute, out validatedUri);
            if (isValid && (validatedUri?.Scheme == Uri.UriSchemeHttp || validatedUri?.Scheme == Uri.UriSchemeHttps))
            {
                var fileContent = await this.httpClient.GetStringAsync(parameters.DocumentationFile);
                var bytes = System.Text.Encoding.UTF8.GetBytes(fileContent);
                content = Convert.ToBase64String(bytes);
            }
            else
            {
                if (!File.Exists(parameters.DocumentationFile))
                {
                    throw new FileNotFoundException($"Documentation file not found: {parameters.DocumentationFile}");
                }

                var bytes = File.ReadAllBytes(parameters.DocumentationFile);
                content = Convert.ToBase64String(bytes);
            }

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
