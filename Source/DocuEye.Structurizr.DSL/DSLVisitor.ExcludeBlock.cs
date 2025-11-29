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
        public IEnumerable<string> VisitExcludeBlocks(StructurizrDSLParser.ExcludeBlockContext[]? excludeBlocks)
        {
            var excludes = new List<string>();
            if (excludeBlocks == null)
            {
                return excludes;
            }
            foreach (var excludeBlock in excludeBlocks)
            {
                var exclude = (List<string>)this.VisitExcludeBlock(excludeBlock);
                excludes.AddRange(exclude);
            }
            return excludes;
        }
        public override object VisitExcludeBlock([NotNull] StructurizrDSLParser.ExcludeBlockContext context)
        {
            List<string> values = new List<string>();
            var valueContexts = context.excludeValue();
            foreach (var valueContext in valueContexts)
            {
                var value = valueContext.GetText().Trim('"');
                values.Add(value);
            }
            return values;
        }
    }
}
