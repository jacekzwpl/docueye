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
        public override object VisitStylePositionBlock([NotNull] StructurizrDSLParser.StylePositionBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            int position = 0;
            if (!int.TryParse(value, out position))
            {
                throw new Exception(
                    string.Format(
                        "Invalid position value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            if (position < 0 || position > 100)
            {
                throw new Exception(
                    string.Format(
                        "Invalid position value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            return position;
        }
    }
}
