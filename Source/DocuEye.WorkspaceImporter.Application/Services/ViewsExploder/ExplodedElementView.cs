using DocuEye.ViewsKeeper.Model;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Element in view exploded from structurizr json
    /// </summary>
    public class ExplodedElementView : ElementView
    {
        /// <summary>
        /// The ID of element in structurizr json
        /// </summary>
        public string StructurizrId { get; set; } = null!;
    }
}
