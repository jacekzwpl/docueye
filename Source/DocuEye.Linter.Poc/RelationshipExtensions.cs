using DocuEye.Linter.Model;
using System.Linq.Dynamic.Core.CustomTypeProviders;

namespace DocuEye.Linter.Poc
{
    [DynamicLinqType]
    public static class RelationshipExtensions
    {
        public static bool IsCycle(this LinterModelRelationship relationship, string tag, IEnumerable<LinterModelRelationship> allRelationships)
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
