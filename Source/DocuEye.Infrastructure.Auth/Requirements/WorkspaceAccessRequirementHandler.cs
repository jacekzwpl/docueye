using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace DocuEye.Infrastructure.Auth.Requirements
{
    public class WorkspaceAccessRequirementHandler : AuthorizationHandler<WorkspaceAccessRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public WorkspaceAccessRequirementHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorkspaceAccessRequirement requirement)
        {
            var httpC = httpContextAccessor.HttpContext;
            var accessToken =  httpC.GetTokenAsync("access_token").GetAwaiter().GetResult(); 
            if (!requirement.IsEnabled)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var claims = context.User.Claims;
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
