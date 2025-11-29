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
        public override object VisitStyleThicknessBlock([NotNull] StructurizrDSLParser.StyleThicknessBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            int width = 0;
            if (!int.TryParse(value, out width))
            {
                throw new Exception(
                    string.Format(
                        "Invalid width value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            return width;
        }
    }
}
