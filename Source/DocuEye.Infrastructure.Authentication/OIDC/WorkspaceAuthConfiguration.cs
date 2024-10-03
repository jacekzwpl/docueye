using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Authentication.OIDC
{
    public class WorkspaceAuthConfiguration
    {
        public IEnumerable<string> Names { get; set; } = Enumerable.Empty<string>();
        public bool IsPrivate { get; set; } = false;
    }
}
