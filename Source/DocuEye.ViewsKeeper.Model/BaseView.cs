using DocuEye.Infrastructure.Persistence.Model;

namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// View with common properties
    /// </summary>
    public class BaseView : BaseEntity
    {
        /// <summary>
        /// The title of this view (optional).
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// The description of this view.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// A unique identifier for this view.
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// The ID of workspace id to which view belongs
        /// </summary>
        public string WorkspaceId { get; set; } = string.Empty;
        /// <summary>
        /// The View Type
        /// </summary>
        public string ViewType { get; set; } = string.Empty;
    }
}
