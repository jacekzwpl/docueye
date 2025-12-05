using AutoMapper.Execution;
using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.Compatibility
{
    public class CompatibilityCheckService : ICompatibilityCheckService
    {
        private readonly IDocuEyeApiClient apiClient;
        private readonly ILogger<CompatibilityCheckService> logger;

        public CompatibilityCheckService(IDocuEyeApiClient apiClient, ILogger<CompatibilityCheckService> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
        }

        string GetProductVersion()
        {
            var path = Environment.ProcessPath;
            var fvi = FileVersionInfo.GetVersionInfo(path!);
            return fvi.ProductVersion ?? "0.0.0";
        }

        public async Task<bool> CheckCompatibility()
        {
            var acceptedVersions = await this.apiClient.CompatibilityInfo();
            if(string.IsNullOrWhiteSpace(acceptedVersions))
            {
                this.logger.LogError("Could not retrieve accepted versions from the server.");
                return false;
            }
            var version = NuGetVersion.Parse(GetProductVersion());
            var range = VersionRange.Parse(acceptedVersions);
            var versionInfo = string.Format("{0}{1} {2}{3}", 
                range.IsMinInclusive ? ">=" : ">",
                range.MinVersion?.Version.ToString(),
                range.HasUpperBound ? range.IsMaxInclusive ? "<=" : "<" : string.Empty,
                range.HasUpperBound ? range.MaxVersion?.Version.ToString() : string.Empty);

            this.logger.LogInformation("Checking CLI version {version} against accepted versions: {acceptedVersions}", version.ToString(), versionInfo);

            var result = range.Satisfies(version);
            if (!result) { 
                this.logger.LogError("Incompatible CLI version. Accepted versions are: {acceptedVersions}", versionInfo);
            }
            return result;
            
        }
    }
}
