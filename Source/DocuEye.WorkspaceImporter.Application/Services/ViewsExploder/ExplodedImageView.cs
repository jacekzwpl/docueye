using DocuEye.ViewsKeeper.Model;

namespace DocuEye.WorkspaceImporter.Application.Services.ViewsExploder
{
    /// <summary>
    /// Image view exploded from structurizr json
    /// </summary>
    public class ExplodedImageView
    {
        /// <summary>
        /// The ID of context element in structurizr json
        /// </summary>
        public string? StructurizrElementId { get; set; }
        /// <summary>
        /// Image view data
        /// </summary>
        public ImageView View { get; set; } = new ImageView();
    }
}
