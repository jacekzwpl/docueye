using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override object VisitTagsBlock([NotNull] TagsBlockContext context)
        {
            List<string> tags = new List<string>();
            var tagsContext = context.tags();
            foreach (var tagContext in tagsContext)
            {
                var newTags = (string[])this.VisitTags(tagContext);
                tags.AddRange(newTags);
            }

            return tags;

        }

        public override object VisitTags([NotNull] TagsContext context)
        {
            var tags = new List<string>();
            var tagsString = context.GetText().Trim('"');
            return tagsString.Split(',');
        }
    }
}
