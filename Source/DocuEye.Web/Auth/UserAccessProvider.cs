using DocuEye.Infrastructure.Authentication.OIDC;

namespace DocuEye.Web.Auth
{
    public class UserAccessProvider : IUserAccessProvider
    {
        public Task<bool> HasAccess(string user, string workspaceId)
        {
            if(user == "AliceSmith@email.com" && workspaceId == "638d0822-12c7-4998-8647-9c7af7ad2989")
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
