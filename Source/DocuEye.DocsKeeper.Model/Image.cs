using DocuEye.Infrastructure.Persistence.Model;

namespace DocuEye.DocsKeeper.Model
{
    /// <summary>
    /// Image
    /// </summary>
    public class Image : BaseEntity
    {
        /// <summary>
        /// The name of the image.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The (base64 encoded) content of the image.
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// The image MIME type (e.g. \"image/png\").
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// The ID of workspace that this image belongs to
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// The ID of documentation that this image belongs to
        /// </summary>
        public string DocumentationId { get; set; } = null!;
    }
}
