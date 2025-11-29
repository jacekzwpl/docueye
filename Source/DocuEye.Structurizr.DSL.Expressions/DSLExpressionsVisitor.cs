using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Expressions.Model;
using System.Xml.Linq;
using static StructurizrDSLExpressionsParser;

namespace DocuEye.Structurizr.DSL.Expressions
{
    public partial class DSLExpressionsVisitor : StructurizrDSLExpressionsBaseVisitor<object>
    {

        public override IEnumerable<DSLExpression> VisitGeneralExpression([NotNull] GeneralExpressionContext context)
        {
            List<DSLExpression> expressions = new List<DSLExpression>();
            if(context.children == null || context.children.Count == 0)
            {
                return expressions;
            }

            var currentConector = DSLExpressionConnector.Undefined;

            foreach (var childContext in context.children)
            {
                if(childContext is ExpressionConnectorContext expressionConnectorContext)
                {
                    currentConector = expressionConnectorContext.OR() != null ? DSLExpressionConnector.Or : DSLExpressionConnector.And;
                }

                if (childContext is ElementTypeExprContext elementTypeExprContext)
                {
                    var expression = this.VisitElementTypeExpr(elementTypeExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is ElementParentExprContext elementParentExprContext)
                {
                    var expression = this.VisitElementParentExpr(elementParentExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is ElementTagExprContext elementTagExprContext)
                {
                    var expression = this.VisitElementTagExpr(elementTagExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is ElementTechnologyExprContext elementTechnologyExprContext)
                {
                    var expression = this.VisitElementTechnologyExpr(elementTechnologyExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is ElementPropertiesExprContext elementPropertiesExprContext)
                {
                    var expression = this.VisitElementPropertiesExpr(elementPropertiesExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is ElementWithCouplingsExprContext elementWithCouplingsExprContext)
                {
                    var expression = this.VisitElementWithCouplingsExpr(elementWithCouplingsExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is RelationshipTagExprContext relationshipTagExprContext)
                {
                    var expression = this.VisitRelationshipTagExpr(relationshipTagExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is RelationshipPropertiesExprContext relationshipPropertiesExprContext)
                {
                    var expression = this.VisitRelationshipPropertiesExpr(relationshipPropertiesExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is RelationshipSourceExprContext relationshipSourceExprContext)
                {
                    var expression = this.VisitRelationshipSourceExpr(relationshipSourceExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is RelationshipDestinationExprContext relationshipDestinationExprContext)
                {
                    var expression = this.VisitRelationshipDestinationExpr(relationshipDestinationExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is RelationshipSpecifiedExprContext relationshipSpecifiedExprContext)
                {
                    var expression = this.VisitRelationshipSpecifiedExpr(relationshipSpecifiedExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }

                if (childContext is RelationshipAllExprContext relationshipAllExprContext)
                {
                    var expression = this.VisitRelationshipAllExpr(relationshipAllExprContext);
                    expression.ExpressionConnector = currentConector;
                    expressions.Add(expression);
                }
            }

            return expressions;
        }



        

    }
}
