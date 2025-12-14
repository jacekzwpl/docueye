using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Commands
{
    public class OpenapiCommand : Command
    {
        public OpenapiCommand() : base("openapi", "Commands for working with OpenAPI specifications.")
        {
            this.Subcommands.Add(new OpenapiImportCommand());
            this.Subcommands.Add(new OpenapiDeleteCommand());
        }
    }
}
