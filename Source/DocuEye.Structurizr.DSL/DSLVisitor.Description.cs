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

        public string? VisitDescriptionBlocks(DescriptionBlockContext[]? descriptionBlocks)
        {
            if (descriptionBlocks == null)
            {
                return null;
            }
            if (descriptionBlocks.Length > 1)
            {
                var duplicate = descriptionBlocks[descriptionBlocks.Length - 1];
                throw new Exception(
                    string.Format(
                        "Duplicate description block for workspace at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (descriptionBlocks.Length > 0)
            {
                return descriptionBlocks[0].description()?.GetText().Trim('"');
            }
            return null;
        }
    }
}
