using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace DocuEye.CLI.Commands
{
    public class DocFileCommand : Command
    {
        public DocFileCommand() : base("docfile", "Commands for working with documentation files for components.")
        {
            this.Subcommands.Add(new DocFileImportCommand());
            this.Subcommands.Add(new DocFileDeleteCommand());
        }
    }
}
