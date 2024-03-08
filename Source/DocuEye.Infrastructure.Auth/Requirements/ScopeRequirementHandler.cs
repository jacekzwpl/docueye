using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Auth.Requirements
{
    public class ScopeRequirementHandler : AuthorizationHandler<ScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
        {
            var scopes = context.User.Claims.Where(o => o.Type == "scope");
            if (scopes.Any(o => o.Value == requirement.ScopeName))
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
