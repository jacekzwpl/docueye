using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.ViewsExploder;
using DocuEye.WorkspaceImporter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector
{
    /// <summary>
    /// Service interface for detecting changes in workspace
    /// </summary>
    public interface IWorkspaceChangeDetectorService
    {
        /// <summary>
        /// Detects elements changes
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="import">Import Id</param>
        /// <param name="newElements">Elements exploded from structurizr json</param>
        /// <param name="existingElements">Elements that exists in workspace</param>
        /// <returns>Result of detecting elements changes </returns>
        Task<ElementsChangesResult> DetectElementsChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements);
        /// <summary>
        /// Detects relationships changes
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="import">Import ID</param>
        /// <param name="newRelationships">Relationships exploded from structurizr json</param>
        /// <param name="existingRelationships">Relationships that exists in workspace</param>
        /// <param name="newElements">Elements exploded from structurizr json</param>
        /// <returns>Result of detecting relationships changes </returns>
        Task<RelaionshipsChangesResult> DetectRelaionshipsChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedRelationship> newRelationships, IEnumerable<Relationship> existingRelationships, IEnumerable<ExplodedElement> newElements);
        /// <summary>
        /// Detects views changes
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="explodeViewsResult">Result of exploding views from structurizr json</param>
        /// <param name="newElements">Elements exploded form structurizr json</param>
        /// <param name="newRelationships">Relationshios exploded from structurizr json</param>
        /// <returns>Result of detecting changes in views</returns>
        ViewsChangesResult DetectViewsChanges(string workspaceId, ViewsExplodeResult explodeViewsResult, IEnumerable<ExplodedElement> newElements, IEnumerable<ExplodedRelationship> newRelationships, IEnumerable<BaseView> existingViews);
        /// <summary>
        /// Detects decisions changes 
        /// </summary>
        /// <param name="worspaceId">The ID of workspace</param>
        /// <param name="documentationId">The ID of documentation that decisions belong to</param>
        /// <param name="elementId">The ID of element that decisions belong to</param>
        /// <param name="newDecisions">Decisions exploded from structurizr json</param>
        /// <returns>List of decisions to save in the workspace</returns>
        IEnumerable<Decision> DetectDecisionChnages(string worspaceId, string documentationId, string? elementId, IEnumerable<ExplodedDecision> newDecisions);
        /// <summary>
        /// Detects changes in images
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="documentationId">The ID of documentation that images belong to</param>
        /// <param name="structurizrImages">List of images form structurizr json</param>
        /// <returns>List of images to save in the workspace</returns>
        IEnumerable<Image> DetectImagesChanges(string workspaceId, string documentationId, IEnumerable<StructurizrImage>? structurizrImages);
        /// <summary>
        /// Detects changes in documentation
        /// </summary>
        /// <param name="workspaceId">The id of workspace</param>
        /// <param name="structurizrDocumentation">Documentation object from structurizr json</param>
        /// <returns>Dcoumentation to save in workspace</returns>
        Documentation DetectDocumentationChanges(string workspaceId, StructurizrDocumentation? structurizrDocumentation);
    }
}
