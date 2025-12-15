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
        public override object VisitStyleBorderBlock([NotNull] StructurizrDSLParser.StyleBorderBlockContext context)
        {
            var allowedValues = new string[] {
                "solid", "dashed", "dotted"
            };

            var value = context.value().GetText().Trim('"');

            if (!allowedValues.Contains(value.ToLower()))
            {
                throw new Exception(
                    string.Format(
                        "Invalid border value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return value;
        }
    }
}
