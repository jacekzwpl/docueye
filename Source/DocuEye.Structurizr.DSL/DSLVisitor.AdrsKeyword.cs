using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public string? VisitAdrsKeyword([NotNull] AdrsKeywordContext[] contexts)
        {
            if (contexts == null) {
                return null;
            }
            if (contexts.Length > 1)
            {
                var duplicate = contexts[1];
                throw new Exception(
                    string.Format(
                        "Duplicate adrs keyword at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (contexts.Length > 0)
            {
                return contexts[0].adrsValue()?.GetText().Trim('"');
            }
            return null;
        }
    }
}
