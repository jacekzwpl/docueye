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
        public async Task<WorkspaceAuthConfiguration> GetWorkspaceAuthConfig(string workspaceId)
        {
            var cacheKey = string.Format("workspace:{0}:auth:config", workspaceId);
            var cachedAuthConfig = this.memoryCache.Get<WorkspaceAuthConfiguration>(cacheKey);
            if(cachedAuthConfig == null)
            {
                var query = new GetWorkspaceQuery(workspaceId);
                var workspace = await this.mediator.Send<Workspace?>(query);
                if (workspace == null)
                {
                    return new WorkspaceAuthConfiguration();
                }
                var namesList = workspace.AccessRules.Select(o => o.Name).ToArray();
                var authConfig = new WorkspaceAuthConfiguration()
                {
                    Names = namesList,
                    IsPrivate = workspace.IsPrivate
                };
                this.memoryCache.Set(cacheKey, authConfig, new TimeSpan(0,5,0));
                return authConfig;
            }else
            {
                return cachedAuthConfig;
            }
            
        }
    }
}
