using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Model;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override string VisitModelElementReference([NotNull] ModelElementReferenceContext context)
        {
            string elementIdentifier;
            var identifierReference = context.identifierReference();
            if (identifierReference != null)
            {
                elementIdentifier = identifierReference.GetText().Trim('"');
            }
            else
            {
                throw new Exception(
                    string.Format(
                        "Element identifier is missing at {0}:{1}",
                        context.Start.Line,
                        context.Start.Column));
            }

            var bodyContext = context.modelElementBody();

            // element reference
            var modelElement = this.workspace.Model.GetModelElement(elementIdentifier);
            if (modelElement != null && (context.EXREF() != null || context.EXELEMENT() != null))
            {
                this.VisitModelElementBody(bodyContext, modelElement);
                return elementIdentifier;
            }
            // deployment environment reference
            var deploymentEnvironment = this.workspace.Model.GetDeploymentEnvironment(elementIdentifier);
            if (deploymentEnvironment != null && (context.EXREF() != null || context.EXELEMENT() != null))
            {
                this.VisitModelElementBody(bodyContext, deploymentEnvironment.ToModelElement());
                return elementIdentifier;
            }
            // relationship reference
            var relationship = this.workspace.Model.GetRelationship(elementIdentifier);
            if (relationship != null && (context.EXREF() != null || context.EXRELATIONSHIP() != null)) { 
                this.VisitModelElementBody(bodyContext, relationship.ToModelElement());
                return elementIdentifier;
            }


            throw new Exception(
                            string.Format(
                                "Identifier '{0}' does not exists in model at {1}:{2}",
                                elementIdentifier,
                                context.Start.Line,
                                context.Start.Column));
        }

    }
}
