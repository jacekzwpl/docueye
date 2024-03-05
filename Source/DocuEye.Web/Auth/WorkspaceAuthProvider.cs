using DocuEye.Infrastructure.Auth;
using DocuEye.WorkspacesKeeper.Application.Queries.GetWorkspace;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DocuEye.Web.Auth
{
    public class WorkspaceAuthProvider : IWorkspaceAuthProvider
    {
        private IMediator mediator;
        private IMemoryCache memoryCache;

        public WorkspaceAuthProvider(IMediator mediator, IMemoryCache memoryCache)
        {
            this.mediator = mediator;
            this.memoryCache = memoryCache;
        }
        public async Task<IEnumerable<string>> GetWorkspaceUsers(string workspaceId)
        {
            var query = new GetWorkspaceQuery(workspaceId);
            var workspace = await this.mediator.Send<Workspace?>(query);
            if(workspace == null)
            {
                return new string[] {  };
            }
            return workspace.AccessRules.Select(o => o.Name).ToArray();
        }
    }
}
