namespace DocuEye.WorkspaceImporter.Api.Model.Docs
{
    public class ImageToImport
    {
        /// <summary>
        /// DocuEye ID of the documentation.
        /// </summary>
        public string DocumentationId { get; set; } = null!;
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
    }
}
