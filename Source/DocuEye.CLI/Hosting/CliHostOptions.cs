using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Hosting
{
    public class CliHostOptions
    {
        public string DocueyeAddress { get; set; } = string.Empty;
        public string AdminToken { get; set; } = string.Empty;

        public CliHostOptions(string docueyeAddress, string adminToken)
        {
            DocueyeAddress = docueyeAddress;
            AdminToken = adminToken;
        }
    }
}
