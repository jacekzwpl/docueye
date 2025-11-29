using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Expressions;
using DocuEye.Structurizr.DSL.Expressions.Builders;
using DocuEye.Structurizr.DSL.Expressions.Model;
using DocuEye.Structurizr.DSL.Model;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {

        public override List<StructurizrModelElement> VisitBulkModelElements([NotNull] BulkModelElementsContext context)
        {
            var expressions = new List<DSLExpression>();
            var expressionValue = context.expressionValue();
            if (expressionValue != null) {
                var parser = this.CreateExpressionsParserFromText(expressionValue.GetText());
                var generalExpressionContext = parser.generalExpression();
                var visitor = new DSLExpressionsVisitor();
                var result = visitor.VisitGeneralExpression(generalExpressionContext);
                expressions.AddRange(result);
            }

            var elementsExpressionBuilder = new ElementExpressionBuilder(this.workspace.Model.Relationships);
            var elementsExpression = elementsExpressionBuilder.BuildExpression(expressions);
            var elements = this.workspace.Model.Elements.AsQueryable()
                .Where(elementsExpression).ToList();

            var bodyContext = context.modelElementBody();
            if (bodyContext != null) {
                foreach (var element in elements)
                {
                    var modelElement = this.VisitModelElementBody(bodyContext, element);
                }
            }
            

            return elements;
           
        }
    }
}
