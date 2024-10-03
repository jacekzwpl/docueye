using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Authentication.OIDC
{
    public interface IUserAccessProvider
    {
        Task<bool> HasAccess(string user, string workspaceId);
    }
}
