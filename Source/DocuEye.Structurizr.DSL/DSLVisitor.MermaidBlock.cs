using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public string? VisitMermaidBlocks(StructurizrDSLParser.MermaidBlockContext[]? contexts)
        {
            if (contexts == null)
            {
                return null;
            }
            if (contexts.Length == 0)
            {
                return null;
            }
            if (contexts.Length > 1)
            {
                var duplicate = contexts[contexts.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate mermaid block at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }

            return VisitMermaidBlock(contexts[0]);
        }
        public override string VisitMermaidBlock([NotNull] StructurizrDSLParser.MermaidBlockContext context)
        {
            return context.mermaidValue().GetText().Trim('"');
        }
    }
}
