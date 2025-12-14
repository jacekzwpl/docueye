using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {

        public StructurizrDeploymentNode VisitDeploymentNode([NotNull] DeploymentNodeContext context, string environmentIdentifier, string? parentIdentifier)
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

            if (this.workspace.Model.AreIdentifiersHierarchical && !string.IsNullOrEmpty(parentIdentifier))
            {
                identifier = parentIdentifier + "." + identifier;
            }else if (this.workspace.Model.AreIdentifiersHierarchical)
            {
                identifier = environmentIdentifier + "." + identifier;
            }

            var element = new StructurizrDeploymentNode(identifier, environmentIdentifier, parentIdentifier, new string[] {
                "Element", "Deployment Node"
            });
         

            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Name is required for deployment node at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(
                    string.Format(
                        "Name is required for deployment node at {0}:{1}",
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

            var instancesContext = context.instances();
            if (instancesContext != null)
            {
                var instances = instancesContext.GetText().Trim('"');
                element.Instances = instances;
            }

            var bodyContext = context.modelElementBody();
            if (bodyContext != null)
            {
                var modelElement = this.VisitModelElementBody(bodyContext, element.ToModelElement());
                element.FromModelElement(modelElement);
            }

            return element;
        }

        
    }
}
