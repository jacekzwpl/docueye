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
        public override object VisitStyleDescriptionBlock([NotNull] StructurizrDSLParser.StyleDescriptionBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            bool description = true;
            if (!bool.TryParse(value, out description))
            {
                throw new Exception(
                    string.Format(
                        "Invalid description value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return description;
        }
    }
}
