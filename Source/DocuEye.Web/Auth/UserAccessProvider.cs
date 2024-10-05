using DocuEye.Infrastructure.Authentication.OIDC;
using DocuEye.WorkspacesKeeper.Application.Queries.GetWorkspace;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace DocuEye.Web.Auth
{
    public class UserAccessProvider : IUserAccessProvider
    {
        private readonly IMediator mediator;
        private IMemoryCache memoryCache;

        public UserAccessProvider(IMediator mediator, IMemoryCache memoryCache)
        {
            this.mediator = mediator;
            this.memoryCache = memoryCache;
        }
        public async Task<bool> HasAccess(string user, string workspaceId)
        {

            var cacheKey = string.Format("workspace:{0}:auth:config", workspaceId);
            var cachedAuthConfig = this.memoryCache.Get<WorkspaceAuthConfiguration>(cacheKey);

            if(cachedAuthConfig != null)
            {
                return this.CheckAccess(cachedAuthConfig, user);
            }
            else
            {
                var query = new GetWorkspaceQuery(workspaceId);
                var workspace = await this.mediator.Send<Workspace?>(query);
                if (workspace == null)
                {
                    return false;
                }
                var namesList = workspace.AccessRules.Select(o => o.Name).ToArray();
                var authConfig = new WorkspaceAuthConfiguration()
                {
                    Names = namesList,
                    IsPrivate = workspace.IsPrivate
                };
                this.memoryCache.Set(cacheKey, authConfig, new TimeSpan(0, 5, 0));
                return this.CheckAccess(authConfig, user);
            }
        }

        private bool CheckAccess(WorkspaceAuthConfiguration config, string user)
        {
            if(!config.IsPrivate)
            {
                return true;
            }

            if(config.Names.Contains(user))
            {
                return true;
            }

            return false;
        }
    }
}
