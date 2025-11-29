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
        public string? VisitInstancesBlocks(InstancesBlockContext[]? contexts)
        {
            if (contexts == null)
            {
                return null;
            }
            if (contexts.Length > 1)
            {
                var duplicate = contexts[1];
                throw new Exception(
                    string.Format(
                        "Duplicate instances block at {0}:{1}",
                        duplicate.Start.Line,
                        duplicate.Start.Column));
            }
            if (contexts.Length > 0)
            {
                return contexts[0].instances()?.GetText().Trim('"');
            }
            return null;
        }
    }
}
