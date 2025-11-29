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

        public StructurizrDeploymentGroup VisitDeploymentGroup([NotNull] StructurizrDSLParser.DeploymentGroupContext context, string environmentIdentifier)
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
                identifier = environmentIdentifier + "." + identifier;
            }

            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Name is required for deployment group at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(
                    string.Format(
                        "Name is required for deployment group at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            return new StructurizrDeploymentGroup(identifier, name, environmentIdentifier);
        }

        public IEnumerable<string> VisitDeploymentGroupsRef([NotNull] StructurizrDSLParser.DeploymentGroupsRefContext context)
        {
            var identifiersText = context.GetText().Trim('"');
            var identifiers = identifiersText.Split(',');

            foreach (var identifier in identifiers)
            {
                if(!this.workspace.Model.DeploymentGroupIdentifierExists(identifier))
                {
                    throw new Exception(
                        string.Format(
                            "Deployment Group with identifier {0} does not exist",
                            identifier));
                }
            }
            return identifiers;
        }
        
    }
}
