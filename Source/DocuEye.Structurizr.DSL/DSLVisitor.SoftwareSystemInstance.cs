using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public StructurizrSoftwareSystemInstance VisitSoftwareSystemInstance([NotNull] StructurizrDSLParser.SoftwareSystemInstanceContext context, string deploymentNodeIdentifier)
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

            var systemIdentifierContext = context.identifierReference();
            if (systemIdentifierContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Software System Identifier is required for softwareSystemInstance at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            var systemIdentifier = systemIdentifierContext.GetText().Trim('"');


            if (!this.Workspace.Model.ElementIdentifierExists(systemIdentifier))
            {
                throw new Exception(
                    string.Format(
                        "Software System with identifier {0} does not exist for softwareSystemInstance at {1}:{2}",
                        systemIdentifier,
                        context.Start.Line,
                        context.Start.Column));
            }


            var instance = new StructurizrSoftwareSystemInstance(identifier, systemIdentifier, deploymentNodeIdentifier, new string[]
            {
                "Software System Instance"
            });

            var deploymentGroupContext = context.deploymentGroupsRef();
            if(deploymentGroupContext != null)
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
                var instanceBody = this.VisitSoftwareSystemInstanceBody(bodyContext, identifier);
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
