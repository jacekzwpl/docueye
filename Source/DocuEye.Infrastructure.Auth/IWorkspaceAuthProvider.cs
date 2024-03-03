using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Auth
{
    public interface IWorkspaceAuthProvider
    {
        IEnumerable<string> GetWorkspaceUsers(string workspaceId);
    }
}
