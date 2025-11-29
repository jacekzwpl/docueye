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
        public string? VisitUrlBlocks(UrlBlockContext[]? urlBlocks)
        {
            if (urlBlocks == null)
            {
                return null;
            }
            if (urlBlocks.Length > 1)
            {
                var duplicate = urlBlocks[1];
                throw new Exception(
                    string.Format(
                        "Duplicate url block for person at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (urlBlocks.Length > 0)
            {
                return this.VisitUrlBlock(urlBlocks[0]).ToString();
            }
            return null;
        }

        public override object VisitUrlBlock([NotNull] UrlBlockContext context)
        {
            var value = context.url().GetText().Trim('"');
            Uri validatedUri;
            var isValid = Uri.TryCreate(value, UriKind.Absolute, out validatedUri);
            if (!isValid)
            {
                throw new Exception(
                    string.Format(
                        "Invalid URL '{0}' at {1}:{2}",
                        value,
                        context.Start.Line,
                        context.Start.Column));
            }
            return value;
        }
    }
}
