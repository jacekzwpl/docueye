using DocuEye.Infrastructure.Persistence.Model;

namespace DocuEye.DocsKeeper.Model
{
    public class DocumentationFile : BaseEntity
    {
        public string WorkspaceId { get; set; } = null!;
        public string ElementId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string MediaType { get; set; } = null!;
    }
}
