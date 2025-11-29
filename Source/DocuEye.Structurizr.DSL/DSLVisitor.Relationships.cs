using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
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

        private void VisitRelationships(RelationshipContext[]? relationshipContexts, string? parentIdentifier = null)
        {
            if (relationshipContexts == null)
            {
                return;
            }

            foreach (var relationshipContext in relationshipContexts)
            {
                var relationship = (StructurizrRelationship)this.VisitRelationship(relationshipContext, parentIdentifier);
                this.workspace.Model.AddRelationship(relationship);
            }
        }

        public object VisitRelationship([NotNull] RelationshipContext context, string? parentIdentifier)
        {

            string identifier;
            var identifierContext = context.identifier();
            if (identifierContext == null)
            {
                identifier = Guid.NewGuid().ToString();
            }
            else
            {
                identifier = identifierContext.GetText().Trim('"');
            }

            var relationship = new StructurizrRelationship(identifier, new string[] { "Relationship" });

            var relationshipSourceContext = context.relationshipSource();
            if (relationshipSourceContext != null)
            {
                var source = relationshipSourceContext.GetText().Trim('"');
                relationship.SourceIdentifier = source == "this" ? null : source;
            }
            var relationshipTargetContext = context.relationshipTarget();
            if (relationshipTargetContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Target is required for relationship at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var target = relationshipTargetContext.GetText().Trim('"');
            relationship.DestinationIdentifier = target;

            var descriptionContext = context.description();
            if (descriptionContext != null)
            {
                relationship.Description = descriptionContext.GetText().Trim('"');
            }

            var technologyContext = context.technology();
            if (technologyContext != null)
            {
                relationship.Technology = technologyContext.GetText().Trim('"');
            }

            var tagsContext = context.tags();
            if (tagsContext != null)
            {
                var tags = (string[])this.VisitTags(tagsContext);
                relationship.Tags.AddRange(tags);
            }


            var relationshipBodyContext = context.modelElementBody();
            if (relationshipBodyContext != null)
            {
                var modelElement = this.VisitModelElementBody(relationshipBodyContext, relationship.ToModelElement());
                relationship.FromModelElement(modelElement);
                /*
                var relationshipBody = (StructurizrRelationship)this.VisitRelationshipBody(relationshipBodyContext);
                relationship.Tags.AddRange(relationshipBody.Tags);
                if (relationship.Description == null)
                {
                    relationship.Description = relationshipBody.Description;
                }
                relationship.Properties = relationshipBody.Properties;
                relationship.Url = relationshipBody.Url;
                if (relationship.Technology == null)
                {
                    relationship.Technology = relationshipBody.Technology;
                }*/
            }

            if (relationship.SourceIdentifier == null && parentIdentifier == null)
            {
                throw new Exception(
                    string.Format(
                        "Source identifier is required for relationship at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            else if (relationship.SourceIdentifier == null)
            {
                relationship.SourceIdentifier = parentIdentifier;
            }
            else
            {
                if (relationship.SourceIdentifier != parentIdentifier && !this.workspace.Model.ElementIdentifierExists(relationship.SourceIdentifier))
                {
                    throw new Exception(
                        string.Format(
                            "Source identifier '{0}' does not exists at {1}:{2}",
                            relationship.SourceIdentifier,
                            context.Start.Line,
                            context.Start.Column));
                }
            }

            if (!this.workspace.Model.ElementIdentifierExists(relationship.DestinationIdentifier))
            {
                throw new Exception(
                    string.Format(
                        "Destination identifier '{0}' does not exists at {1}:{2}",
                        relationship.DestinationIdentifier,
                        context.Start.Line,
                        context.Start.Column));
            }

            return relationship;
        }

        
    }
}
