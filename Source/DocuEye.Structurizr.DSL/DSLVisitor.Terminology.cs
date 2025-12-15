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
        public override StructurizrTerminology VisitTerminology([NotNull] StructurizrDSLParser.TerminologyContext context)
        {
            var terminology = new StructurizrTerminology();

            var personContext = context.terminologyPersonBlock();
            if (personContext != null && personContext.Length > 0)
            {
                terminology.Person = (string)this.VisitTerminologyPersonBlock(personContext.First());
            }

            var softwareSystemContext = context.terminologySoftwareSystemBlock();
            if (softwareSystemContext != null && softwareSystemContext.Length > 0)
            {
                terminology.SoftwareSystem = (string)this.VisitTerminologySoftwareSystemBlock(softwareSystemContext.First());
            }

            var containerContext = context.terminologyContainerBlock();
            if (containerContext != null && containerContext.Length > 0)
            {
                terminology.Container = (string)this.VisitTerminologyContainerBlock(containerContext.First());
            }

            var componentContext = context.terminologyComponentBlock();
            if (componentContext != null && componentContext.Length > 0)
            {
                terminology.Component = (string)this.VisitTerminologyComponentBlock(componentContext.First());
            }

            var relationshipContext = context.terminologyRelationshipBlock();
            if (relationshipContext != null && relationshipContext.Length > 0)
            {
                terminology.Relationship = (string)this.VisitTerminologyRelationshipBlock(relationshipContext.First());
            }

            var deploymentNodeContext = context.terminologyDeploymentNodeBlock();
            if (deploymentNodeContext != null && deploymentNodeContext.Length > 0)
            {
                terminology.DeploymentNode = (string)this.VisitTerminologyDeploymentNodeBlock(deploymentNodeContext.First());
            }

            var infrastructureNodeContext = context.terminologyInfrastructureNodeBlock();
            if (infrastructureNodeContext != null && infrastructureNodeContext.Length > 0)
            {
                terminology.InfrastructureNode = (string)this.VisitTerminologyInfrastructureNodeBlock(infrastructureNodeContext.First());
            }
            return terminology;
        }

        public override object VisitTerminologyPersonBlock([NotNull] StructurizrDSLParser.TerminologyPersonBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }

        public override object VisitTerminologySoftwareSystemBlock([NotNull] StructurizrDSLParser.TerminologySoftwareSystemBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }

        public override object VisitTerminologyContainerBlock([NotNull] StructurizrDSLParser.TerminologyContainerBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }

        public override object VisitTerminologyComponentBlock([NotNull] StructurizrDSLParser.TerminologyComponentBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }

        public override object VisitTerminologyRelationshipBlock([NotNull] StructurizrDSLParser.TerminologyRelationshipBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }

        public override object VisitTerminologyDeploymentNodeBlock([NotNull] StructurizrDSLParser.TerminologyDeploymentNodeBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }

        public override object VisitTerminologyInfrastructureNodeBlock([NotNull] StructurizrDSLParser.TerminologyInfrastructureNodeBlockContext context)
        {
            var value = context.value().GetText().Trim('"');
            return value;
        }
    }
}
