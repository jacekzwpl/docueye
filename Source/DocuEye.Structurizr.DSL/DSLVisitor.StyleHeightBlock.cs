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
        public override object VisitStyleHeightBlock([NotNull] StructurizrDSLParser.StyleHeightBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            int height = 0;
            if (!int.TryParse(value, out height))
            {
                throw new Exception(
                    string.Format(
                        "Invalid height value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return height;
        }
    }
}
