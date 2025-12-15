using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.Compatibility
{
    public interface ICompatibilityCheckService
    {
        Task<bool> CheckCompatibility();
    }
}
