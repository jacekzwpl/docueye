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
        public override object VisitStyleIconBlock([NotNull] StructurizrDSLParser.StyleIconBlockContext context)
        {

            var value = context.imageValue().GetText().Trim('"');
            
            return value;
        }
    }
}
