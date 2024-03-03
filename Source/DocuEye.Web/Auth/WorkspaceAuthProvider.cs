using DocuEye.Infrastructure.Auth;

namespace DocuEye.Web.Auth
{
    public class WorkspaceAuthProvider : IWorkspaceAuthProvider
    {
        public IEnumerable<string> GetWorkspaceUsers(string workspaceId)
        {
            return new string[] { "TeamAe" };
        }
    }
}
