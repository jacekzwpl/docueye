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
        public string? VisitPlantumlBlocks(StructurizrDSLParser.PlantumlBlockContext[]? contexts)
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
                        "Duplicate plantuml block at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }

            return VisitPlantumlBlock(contexts[0]);
        }
        public override string VisitPlantumlBlock([NotNull] StructurizrDSLParser.PlantumlBlockContext context)
        {
            return context.plantumlValue().GetText().Trim('"');
        }
    }
}
