using Microsoft.AspNetCore.Authorization;

namespace DocuEye.Infrastructure.Authentication.OIDC
{
    public class GeneralAccessRequirement : IAuthorizationRequirement
    {
        public bool IsEnabled { get; private set; }
        public GeneralAccessRequirement(bool isEnabled)
        {
            this.IsEnabled = isEnabled;
        }
    }
}
