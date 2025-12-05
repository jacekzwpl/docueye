
using DocuEye.CLI.ApiClient.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System.Threading.Tasks;

namespace DocuEye.CLI.ApiClient
{
    /// <summary>
    /// DocueEye Ap client interface
    /// </summary>
    public interface IDocuEyeApiClient
    {


        Task<ImportWorkspaceResponse> ImportStart(ImportWorkspaceStartRequest data);
        Task<ImportWorkspaceResponse> ImportViewConfiguration(ImportViewConfigurationRequest data);
        Task<ImportWorkspaceResponse> ImportElements(ImportElementsRequest data);
        Task<ImportWorkspaceResponse> ImportRelationships(ImportRelationshipsRequest data);
        Task<ImportWorkspaceResponse> ImportViews(ImportViewsRequest data);
        Task<ImportWorkspaceResponse> ImportClearDocItems(ImportClearDocItemsRequest data);
        Task<ImportWorkspaceResponse> ImportDocumentation(ImportDocumentationRequest data);
        Task<ImportWorkspaceResponse> ImportDecision(ImportDecisionRequest data);
        Task<ImportWorkspaceResponse> ImportImage(ImportImageRequest data);
        Task<ImportWorkspaceResponse> ImportDecisionLinks(ImportDecisionsLinksRequest data);
        Task<ImportWorkspaceResponse> ImportClearDecisions(ImportClearDecisionsRequest data);
        Task<ImportWorkspaceResponse> ImportFinish(ImportFinalizeRequest data);

        Task<string?> ImportOpenApiFile(string workspaceId, ImportOpenApiFileRequest request);
        Task<string?> DeleteWorkspace(string workspaceId);

        Task<string?> CompatibilityInfo();

    }
}
