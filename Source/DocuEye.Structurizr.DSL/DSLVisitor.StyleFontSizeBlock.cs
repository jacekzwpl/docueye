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
        public override object VisitStyleFontSizeBlock([NotNull] StructurizrDSLParser.StyleFontSizeBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            int fontSize = 0;
            if (!int.TryParse(value, out fontSize))
            {
                throw new Exception(
                    string.Format(
                        "Invalid fontSize value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return fontSize;
        }
    }
}
