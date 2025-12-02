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

        public StructurizrContainerInstance VisitContainerInstance([NotNull] StructurizrDSLParser.ContainerInstanceContext context, string deploymentNodeIdentifier)
        {
            //var deploymentNode = this.Workspace.Model.GetModelElement(deploymentNodeIdentifier);

            //if (deploymentNode == null)
            //{
            //    throw new Exception(
            //        string.Format(
            //            "Deployment Node with identifier {0} does not exist for containerInstance at {1}:{2}",
            //            deploymentNodeIdentifier,
            //            context.Start.Line,
            //            context.Start.Column));
            //}
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

            var containerIdentifierContext = context.identifierReference();
            if (containerIdentifierContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Container Identifier is required for containerInstance at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            var containerIdentifier = containerIdentifierContext.GetText().Trim('"');


            if (!this.Workspace.Model.ElementIdentifierExists(containerIdentifier))
            {
                throw new Exception(
                    string.Format(
                        "Container Identifier with identifier {0} does not exist for containerInstance at {1}:{2}",
                        containerIdentifier,
                        context.Start.Line,
                        context.Start.Column));
            }


            var instance = new StructurizrContainerInstance(identifier, containerIdentifier, deploymentNodeIdentifier, new string[]
            {
                "Element", "Container Instance"
            });
            
            //instance.DeploymentEnvironmentIdentifier = deploymentNode.DeploymentEnvironmentIdentifier;
            var deploymentGroupContext = context.deploymentGroupsRef();
            if (deploymentGroupContext != null)
            {
                instance.DeploymentGroupsIdentiifiers = this.VisitDeploymentGroupsRef(deploymentGroupContext);
            }

            var tagsContext = context.tags();
            if (tagsContext != null)
            {
                var tags = (string[])this.VisitTags(tagsContext);
                instance.Tags.AddRange(tags);
            }

            var bodyContext = context.modelElementBody();
            if (bodyContext != null)
            {
                var modelElement = this.VisitModelElementBody(bodyContext, instance.ToModelElement());
                instance.FromModelElement(modelElement);
                /*
                var instanceBody = this.VisitContainerInstanceBody(bodyContext, identifier);
                instance.Description = instanceBody.Description;
                instance.Tags.AddRange(instanceBody.Tags);
                instance.Properties = instanceBody.Properties;
                instance.Url = instanceBody.Url;
                instance.Perspectives = instanceBody.Perspectives;*/
            }

            return instance;
        }


       
    }
}
