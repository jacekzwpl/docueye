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
        public override IEnumerable<string> VisitThemesBlock([NotNull] StructurizrDSLParser.ThemesBlockContext context)
        {
            var values = new List<string>();
            var valueContexts = context.themesValue();
            if(valueContexts != null)
            {
                foreach (var valueContext in valueContexts)
                {
                    values.Add(valueContext.GetText().Trim('"'));
                }
            }

            return values;
        }
    }
}
