using CommandLine;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI
{
    public class CommandLineCommonOptions
    {
        public Option<string> DocueyeAddressOption { get; private set; } = null!;
        public Option<string> AdminTokenOption { get; private set; } = null!;
        public CommandLineCommonOptions()
        {
            CreateOptions();
        }

        public void CreateOptions()
        {

            this.DocueyeAddressOption = new("--docueye-address", "-a")
            {
                Description = "DocuEye address ex. http://localhost:8080.",
                Required = true
            };

            this.AdminTokenOption = new("--admin-token", "-t")
            {
                Description = "The Admin token from DocuEye configuration.",
                Required = true
            };

        }
    }
}
