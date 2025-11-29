using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override object VisitDeploymentEnvironment([NotNull] DeploymentEnvironmentContext context)
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

            var nameContext = context.name();
            if (nameContext == null)
            {
                throw new Exception(
                    string.Format(
                        "Name is required for deployment environment at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }
            var name = nameContext.GetText().Trim('"');
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(
                    string.Format(
                        "Name is required for deployment environment at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            var element = new StructurizrDeploymentEnvironment(identifier, name);
            var bodyContext = context.modelElementBody();
            if(bodyContext != null)
            {
                var modelElement = this.VisitModelElementBody(bodyContext, element.ToModelElement());
                element.FromModelElement(modelElement);
                //this.VisitDeploymentEnvironmentBody(bodyContext, identifier);
            }

            return element;
        }

        
    }
}
