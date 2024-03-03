using Microsoft.AspNetCore.Authorization;

namespace DocuEye.Infrastructure.Auth.Requirements
{
    public class WorkspaceAccessRequirement : IAuthorizationRequirement
    {
        public bool IsEnabled { get; private set; }
        public WorkspaceAccessRequirement(bool isEnabled) { 
            this.IsEnabled = isEnabled;
        }
    }
}
