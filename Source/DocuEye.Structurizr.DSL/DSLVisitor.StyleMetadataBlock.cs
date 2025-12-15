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
        public override object VisitStyleMetadataBlock([NotNull] StructurizrDSLParser.StyleMetadataBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            bool metadata = true;
            if (!bool.TryParse(value, out metadata))
            {
                throw new Exception(
                    string.Format(
                        "Invalid metadata value at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return metadata;
        }
    }
}
