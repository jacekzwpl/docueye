using DocuEye.Infrastructure.Persistence.Model;

namespace DocuEye.ViewsKeeper.Model
{
    public class ViewLayout : BaseEntity
    {
        public string WorkspaceId { get; set; } = null!;

        public string LayoutData { get; set; } = null!;
    }
}
