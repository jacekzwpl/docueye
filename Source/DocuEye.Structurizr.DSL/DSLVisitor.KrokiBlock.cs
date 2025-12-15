using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public StructurizrKrokiConfig? VisitKrokiBlocks(StructurizrDSLParser.KrokiBlockContext[]? contexts)
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
                        "Duplicate kroki block at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }

            return VisitKrokiBlock(contexts[0]);
        }
        public override StructurizrKrokiConfig VisitKrokiBlock([NotNull] StructurizrDSLParser.KrokiBlockContext context)
        {
            var value = context.krokiValue().GetText().Trim('"');
            var format = context.krokiFormat().GetText().Trim('"');
            return new StructurizrKrokiConfig(format, value);
        }
    }
}
