using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace DocuEye.CLI.Commands
{
    public class DocFileDeleteCommand : Command
    {
        public DocFileDeleteCommand() : base("delete", "Delete documentation file for element.")
        {
        }
    }
}
