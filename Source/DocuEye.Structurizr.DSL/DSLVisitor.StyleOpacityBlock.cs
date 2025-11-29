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
        public override object VisitStyleOpacityBlock([NotNull] StructurizrDSLParser.StyleOpacityBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            int opacity = 0;
            if (!int.TryParse(value, out opacity))
            {
                throw new Exception(
                    string.Format(
                        "Invalid opacity value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            if (opacity < 0 || opacity > 100)
            {
                throw new Exception(
                    string.Format(
                        "Invalid opacity value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            return opacity;
        }
    }
}
