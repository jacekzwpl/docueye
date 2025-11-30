using Antlr4.Runtime;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using DocuEye.Structurizr.DSL;
using DocuEye.Structurizr.DSL.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocuEye.CLI.Application.Services.DSL
{
    public class WorkspaceParserService : IWorkspaceParserService
    {
        private readonly ILogger<WorkspaceParserService> logger;
        public WorkspaceParserService(ILogger<WorkspaceParserService> logger)
        {
            this.logger = logger;
        }

        public StructurizrWorkspace? Parse(FileInfo file)
        {
            StructurizrDSLParser parser = CreateParser(file);
            parser.ErrorListeners.Clear();
            //parser.AddErrorListener(new StructurizrDSLErrorListener(this.logger, file.Name));
            var visitor = new DSLVisitor();
            var context = parser.workspace();
            if(parser.NumberOfSyntaxErrors > 0 ) {
                this.logger.LogError("Failed to parse workspace file {FileName} due to syntax errors.", file.Name);
                return null;
            }
            try
            {
                return (StructurizrWorkspace)visitor.VisitWorkspace(context);

            }
            catch (Exception ex)
            {
                this.logger.LogError("{Message}", ex.Message);
                return null;
            }
        }

        protected StructurizrDSLParser CreateParser(FileInfo file)
        {
            string text = File.ReadAllText(file.FullName);
            return CreateParserFromText(text);
        }

        protected StructurizrDSLParser CreateParserFromText(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            StructurizrDSLLexer lexer = new StructurizrDSLLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            return new StructurizrDSLParser(commonTokenStream);

        }


    }
}
