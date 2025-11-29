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
        public IEnumerable<string> VisitIncludeBlocks(StructurizrDSLParser.IncludeBlockContext[]? includeBlocks)
        {
            var includes = new List<string>();
            if (includeBlocks == null)
            {
                return includes;
            }
            foreach (var includeBlock in includeBlocks)
            {
                var include = (List<string>)this.VisitIncludeBlock(includeBlock);
                includes.AddRange(include);
            }
            return includes;
        }

        public override object VisitIncludeBlock([NotNull] StructurizrDSLParser.IncludeBlockContext context)
        {
            List<string> values = new List<string>();
            var valueContexts = context.includeValue();
            foreach (var valueContext in valueContexts)
            {
                var value = valueContext.GetText().Trim('"');
                values.Add(value);
            }
            return values;
        }
    }
}
