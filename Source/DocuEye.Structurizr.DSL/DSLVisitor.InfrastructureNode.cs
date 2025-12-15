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

        public StructurizrInfrastructureNode VisitInfrastructureNode([NotNull] InfrastructureNodeContext context, string deploymentNodeIdentifier)
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

            if (this.workspace.Model.AreIdentifiersHierarchical)
            {
                identifier = deploymentNodeIdentifier + "." + identifier;
            }

            var element = new StructurizrInfrastructureNode(identifier, deploymentNodeIdentifier, new string[] {
                "Element", "Infrastructure Node"
            });
            element.DeploymentNodeIdentifier = deploymentNodeIdentifier;


            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Name is required for infrastructure node at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(
                    string.Format(
                        "Name is required for infrastructure node at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            element.Name = name;

            var descriptionContext = context.description();
            if (descriptionContext != null)
            {
                var description = descriptionContext.GetText().Trim('"');
                element.Description = description;
            }

            var technologyContext = context.technology();
            if (technologyContext != null)
            {
                var technology = technologyContext.GetText().Trim('"');
                element.Technology = technology;
            }


            var tagsContext = context.tags();
            if (tagsContext != null)
            {
                var tags = (string[])this.VisitTags(tagsContext);
                element.Tags.AddRange(tags);
            }

            var bodyContext = context.modelElementBody();
            if (bodyContext != null)
            {
                var modelElement = this.VisitModelElementBody(bodyContext, element.ToModelElement());
                element.FromModelElement(modelElement);
                /*
                var containerBody = this.VisitInfrastructureNodeBody(bodyContext, element.Identifier);
                element.Tags.AddRange(containerBody.Tags);
                if (element.Description == null)
                {
                    element.Description = containerBody.Description;
                }
                if (element.Technology == null)
                {
                    element.Technology = containerBody.Technology;
                }
                element.Properties = containerBody.Properties;
                element.Url = containerBody.Url;
                element.Perspectives = containerBody.Perspectives;*/
            }

            return element;
        }
    }
}
