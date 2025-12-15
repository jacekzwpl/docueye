using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public IEnumerable<StructurizrDynamicViewRelationship> VisitDynamicViewRelationshipGroup([NotNull] StructurizrDSLParser.DynamicViewRelationshipGroupContext context, int currentOrder)
        {
            return this.VisitDynamicViewRelationshipGroupBody(context.dynamicViewRelationshipGroupBody(), currentOrder);
        }

        public IEnumerable<StructurizrDynamicViewRelationship> VisitDynamicViewRelationshipGroupBody([NotNull] StructurizrDSLParser.DynamicViewRelationshipGroupBodyContext context, int currentOrder)
        {
            
            List<StructurizrDynamicViewRelationship> relationships = new List<StructurizrDynamicViewRelationship>();

            foreach (var child in context.children)
            {
                if (child is StructurizrDSLParser.DynamicViewRelationshipContext)
                {
                    var relationship = this.VisitDynamicViewRelationship((StructurizrDSLParser.DynamicViewRelationshipContext)child);
                    if(relationship.Order == 0)
                    {
                        relationship.Order = currentOrder;
                        currentOrder++;
                    }
                    relationships.Add(relationship);
                }
                if(child is StructurizrDSLParser.DynamicViewRelationshipGroupContext)
                {
                    var groupRelationships = this.VisitDynamicViewRelationshipGroup((StructurizrDSLParser.DynamicViewRelationshipGroupContext)child, currentOrder);
                    relationships.AddRange(groupRelationships);
                    currentOrder++;
                }
            }


            return relationships;
        }
    }
}
