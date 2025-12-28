using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.CustomTypeProviders;

namespace DocuEye.Linter
{
    [DynamicLinqType]
    public static class LinterModelRelationshipExtensions
    {
        public static bool IsDirectCycle(this LinterModelRelationship relationship, string tag, IEnumerable<LinterModelRelationship> allRelationships)
        {
            var result = allRelationships
                .Where(o =>
                    o.Source.Identifier == relationship.Destination.Identifier
                    && o.Destination.Identifier == relationship.Source.Identifier
                    && o.Source.Tags.Contains(tag)
                    && o.Destination.Tags.Contains(tag)
                ).Count();
            return result > 0;
        }
    }
}
