using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.DSL
{
    public interface IWorkspaceParserService
    {
        StructurizrWorkspace? Parse(FileInfo file);
    }
}
