using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DocuEye.Infrastructure.Authentication.OIDC
{
    public class WorkspaceAccessRequirementHandler : AuthorizationHandler<WorkspaceAccessRequirement>
    {
        private readonly IUserAccessProvider userAccessProvider;
        private readonly IHttpContextAccessor httpContextAccessor;

        public WorkspaceAccessRequirementHandler(IUserAccessProvider userAccessProvider, IHttpContextAccessor httpContextAccessor)
        {
            this.userAccessProvider = userAccessProvider;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, WorkspaceAccessRequirement requirement)
        {
            if(!requirement.IsEnabled)
            {
                context.Succeed(requirement);
                return;
            }
            var claim = context.User.FindFirst(c => c.Type == requirement.ClaimType);
            if(claim == null)
            {
                context.Fail();
                return;
            }

            var workspaceId = this.httpContextAccessor.HttpContext?.Request.RouteValues["workspaceId"];

            if (workspaceId == null)
            {
                workspaceId = this.httpContextAccessor.HttpContext?.Request.RouteValues["id"];
            }

            if(workspaceId == null)
            {
                context.Fail();
                return;
            }



            var result = await this.userAccessProvider.HasAccess(claim.Value, workspaceId.ToString());


            if (result)
            {
                context.Succeed(requirement);
                
            }else
            {
                context.Fail();
            }
            return;
        }
    }
}
