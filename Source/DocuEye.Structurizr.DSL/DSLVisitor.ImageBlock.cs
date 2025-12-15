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
        public string? VisitImageBlocks(StructurizrDSLParser.ImageBlockContext[]? contexts)
        {
            if (contexts == null)
            {
                return null;
            }
            if(contexts.Length == 0)
            {
                return null;
            }
            if(contexts.Length > 1)
            {
                var duplicate = contexts[contexts.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate image block at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }

            return VisitImageBlock(contexts[0]);
        }
        public override string VisitImageBlock([NotNull] StructurizrDSLParser.ImageBlockContext context)
        {
            return context.imageValue().GetText().Trim('"');
        }
    }
}
