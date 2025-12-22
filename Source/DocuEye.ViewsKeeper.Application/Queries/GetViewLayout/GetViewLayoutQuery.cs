using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.ViewsKeeper.Application.Queries.GetViewLayout
{
    public class GetViewLayoutQuery : IQuery<ViewLayout?>
    {
        public string WorkspaceId { get; }
        public string ViewId { get; }
        public GetViewLayoutQuery(string workspaceId, string viewId)
        {
            WorkspaceId = workspaceId;
            ViewId = viewId;
        }
    }
}
