using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI
{
    public class CommandLineOptions
    {
        [Option('i', "import", Required = true, HelpText = "Set output to verbose messages.")]
        public string Import { get; set; } = null!;

        [Option('a', "docueyeAddress", Required = true, HelpText = "Set output to verbose messages.")]
        public string DocueyeAddress { get; set; } = null!;
        [Option('t', "adminToken", Required = true, HelpText = "Set output to verbose messages.")]
        public string AdminToken { get; set; } = null!;



    }
}
