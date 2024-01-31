
using DocuEye.CLI.ApiClient.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.ApiClient
{
    public interface IDocuEyeApiClient
    {
        Task<ImportWorkspaceResult> ImportWorkspace(ImportWorkspaceRequest request);
    }
}
