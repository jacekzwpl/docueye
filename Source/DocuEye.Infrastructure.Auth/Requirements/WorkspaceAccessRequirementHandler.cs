using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DocuEye.Infrastructure.Auth.Requirements
{
    public class WorkspaceAccessRequirementHandler : AuthorizationHandler<WorkspaceAccessRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWorkspaceAuthProvider workspaceAuthProvider;

        public WorkspaceAccessRequirementHandler(IHttpContextAccessor httpContextAccessor, IWorkspaceAuthProvider workspaceAuthProvider)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.workspaceAuthProvider = workspaceAuthProvider;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorkspaceAccessRequirement requirement)
        {
           
            if (!requirement.IsEnabled)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var workspaceId = this.httpContextAccessor.HttpContext?.Request.RouteValues["id"];
            if(workspaceId == null)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }


            var workspaceUsers = this.workspaceAuthProvider.GetWorkspaceUsers(workspaceId.ToString());
            var roleExists = context.User.Claims.Any(o => o.Type == "roles" && workspaceUsers.Contains(o.Value));
            if(roleExists)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }else
            {
                context.Fail();
                return Task.CompletedTask;
            }
        }
    }
}
