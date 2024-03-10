using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Auth.Requirements
{
    public class ScopeRequirementHandler : AuthorizationHandler<ScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
        {
            var scopes = this.ScopeValues(context.User.Claims.Where(o => o.Type == "scope"));

            if (scopes.Contains(requirement.ScopeName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }else
            {
                context.Fail();
                return Task.CompletedTask;
            }

        }

        private string[] ScopeValues(IEnumerable<Claim> scopeClaims)
        {
            var scopes = new List<string>();
            foreach(var claim in scopeClaims)
            {
                if(claim.Value.Contains(' '))
                {
                    var values = claim.Value.Split(' ');
                    scopes.AddRange(values);
                }
            }
            return scopes.ToArray();
        }
    }
}
