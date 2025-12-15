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
        public override object VisitStyleColorBlock([NotNull] StructurizrDSLParser.StyleColorBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }
    }
}
