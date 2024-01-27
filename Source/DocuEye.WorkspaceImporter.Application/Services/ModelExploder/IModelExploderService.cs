using DocuEye.Structurizr.Model;

namespace DocuEye.WorkspaceImporter.Application.Services.ModelExploder
{
    /// <summary>
    /// Service interface for exploding model from structurizr json
    /// </summary>
    public interface IModelExploderService
    {
        /// <summary>
        /// Explodes model from structurizr json
        /// </summary>
        /// <param name="model">Model object from structurizr json</param>
        /// <returns>The result of exploding model elements and relationships from structurizr json</returns>
        ExplodeResult ExplodeModel(StructurizrModel? model);
    }
}
