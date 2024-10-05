using Microsoft.AspNetCore.Authorization;

namespace DocuEye.Infrastructure.Authentication.OIDC
{
    public class GeneralAccessRequirementHandler : AuthorizationHandler<GeneralAccessRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GeneralAccessRequirement requirement)
        {
            if (!requirement.IsEnabled)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if(context.User.Identity?.IsAuthenticated == true)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
