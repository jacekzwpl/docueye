using DocuEye.Structurizr.Model;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Service interface for exploding views from structurizr json
    /// </summary>
    public interface IViewsExploderService
    {
        /// <summary>
        /// Explodes views from structurizr json
        /// </summary>
        /// <param name="structurizrViews">Views object from structurizr json</param>
        /// <returns>Result of exploding views from structurizr json</returns>
        ViewsExplodeResult Explode(StructurizrViews? structurizrViews);
    }
}
