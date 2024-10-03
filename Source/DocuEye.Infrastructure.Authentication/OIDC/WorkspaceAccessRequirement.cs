using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Authentication.OIDC
{
    public class WorkspaceAccessRequirement : IAuthorizationRequirement
    {
        public bool IsEnabled { get; private set; }
        public string ClaimType { get; private set; }
        public WorkspaceAccessRequirement(bool isEnabled, string? claimType)
        {
            this.IsEnabled = isEnabled;
            ClaimType = string.IsNullOrEmpty(claimType) ? "email" : claimType;
        }
    }
}
