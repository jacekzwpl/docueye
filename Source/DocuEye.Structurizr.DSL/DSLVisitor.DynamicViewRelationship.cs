using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {

        public override StructurizrDynamicViewRelationship VisitDynamicViewRelationship([NotNull] StructurizrDSLParser.DynamicViewRelationshipContext context)
        {
            var orderContext = context.dynamicViewRelationshipOrder();
            int order = 0;
            if(orderContext != null)
            {
                order = (int)this.VisitDynamicViewRelationshipOrder(orderContext);
            }
            var source = string.Empty;
            var target = string.Empty;
            string? technology = null;
            var identifierReferenceContext = context.identifierReference();
            if (identifierReferenceContext != null) { 
                var identifierReference = identifierReferenceContext.GetText().Trim('"');
                var sourceRelationship = this.workspace.Model.Relationships.FirstOrDefault(o => o.Identifier == identifierReference);
                if(sourceRelationship == null)
                {
                    throw new Exception(
                        string.Format(
                            "Relationship with identifier {0} does not exist in the model at {1}:{2}",
                            identifierReference,
                            context.Start.Line,
                            context.Start.Column));
                }
                source = sourceRelationship.SourceIdentifier;
                target = sourceRelationship.DestinationIdentifier;
                technology = sourceRelationship.Technology;
            }
            else
            {
                var sourceContext = context.relationshipSource();
                
                if (sourceContext == null)
                {
                    throw new Exception(
                        string.Format(
                            "Source is required for dynamic view relationship at {0}:{1}",
                            context.Start.Line,
                            context.Start.Column));
                }
                else
                {
                    source = sourceContext.GetText().Trim('"');
                }
                var targetContext = context.relationshipTarget();
                
                if (targetContext == null)
                {
                    throw new Exception(
                        string.Format(
                            "Target is required for dynamic view relationship at {0}:{1}",
                            context.Start.Line,
                            context.Start.Column));
                }
                else
                {
                    target = targetContext.GetText().Trim('"');
                }
            }

            if(!this.workspace.Model.Relationships.Any(
                o => o.SourceIdentifier == source && o.DestinationIdentifier == target
                || o.SourceIdentifier == target && o.DestinationIdentifier == source))
            {
                throw new Exception(
                    string.Format(
                        "Relationship between {0} and {1} does not exist in the model at {2}:{3}",
                        source,
                        target,
                        context.Start.Line,
                        context.Start.Column));
            }

            var relationship = new StructurizrDynamicViewRelationship(
                Guid.NewGuid().ToString(), 
                source, 
                target, 
                order);

            var descriptionContext = context.description();
            if (descriptionContext != null)
            {
                relationship.Description = descriptionContext.GetText().Trim('"');
            }

            var technologyContext = context.technology();
            if (technologyContext != null)
            {
                relationship.Technology = technologyContext.GetText().Trim('"');
            }else
            {
                relationship.Technology = technology;
            }

            return relationship;
        }
    }
}
