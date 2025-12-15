using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DSL.Expressions
{
    public partial class DSLExpressionsVisitor
    {

        public override DSLExpression VisitRelationshipTagExpr([NotNull] StructurizrDSLExpressionsParser.RelationshipTagExprContext context)
        {
            var valueContexts = context.expressionValue();
            var value = string.Empty;
            if (valueContexts != null)
            {
                foreach (var valueContext in valueContexts)
                {
                    if (valueContext != null)
                    {
                        value += " " + valueContext.GetText().Trim();
                    }
                }
            }
            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.RelationshipTag,
                ExpressionValue = value.Trim(),
                ExpressionOperator = context.EQUALS() != null ? DSLExpressionOperator.Equal : DSLExpressionOperator.NotEqual
            };
        }

        public override DSLExpression VisitRelationshipPropertiesExpr([NotNull] StructurizrDSLExpressionsParser.RelationshipPropertiesExprContext context)
        {
            var value = context.expressionValue()?.GetText().Trim();
            var name = context.propertyName()?.GetText().Trim();
            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.RelationshipProperties,
                PropertyName = name,
                ExpressionValue = value,
                ExpressionOperator = context.EQUALS() != null ? DSLExpressionOperator.Equal : DSLExpressionOperator.NotEqual
            };
        }

        public override DSLExpression VisitRelationshipSourceExpr([NotNull] StructurizrDSLExpressionsParser.RelationshipSourceExprContext context)
        {
            var value = context.expressionValue()?.GetText().Trim();
            if(value == null)
            {
                throw new Exception("relationship expression is not valid.");
            }

            if(context.MORE_THAN() != null && value.EndsWith("-"))
            {
                value = value.Substring(0, value.Length - 1);
            }

            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.RelationshipSource,
                ExpressionValue = value,
                ExpressionOperator = DSLExpressionOperator.Equal
            };
        }

        public override DSLExpression VisitRelationshipDestinationExpr([NotNull] StructurizrDSLExpressionsParser.RelationshipDestinationExprContext context)
        {
            var value = context.expressionValue()?.GetText().Trim();
            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.RelationshipDestination,
                ExpressionValue = value,
                ExpressionOperator = DSLExpressionOperator.Equal
            };
        }


        public override DSLExpression VisitRelationshipAllExpr([NotNull] StructurizrDSLExpressionsParser.RelationshipAllExprContext context)
        {
            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.RelationshipAll
            };
        }

        public override DSLExpression VisitRelationshipSpecifiedExpr([NotNull] StructurizrDSLExpressionsParser.RelationshipSpecifiedExprContext context)
        {
            var sourceElement = context.sourceElement()?.GetText().Trim();
            var destinationElement = context.destinationElement()?.GetText().Trim();

            if(string.IsNullOrEmpty(sourceElement) || string.IsNullOrEmpty(destinationElement))
            {
                throw new Exception("relationship expression is not valid.");
            }

            if (context.MORE_THAN() != null && sourceElement.EndsWith("-"))
            {
                sourceElement = sourceElement.Substring(0, sourceElement.Length - 1);
            }

            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.RelationshipSpecified,
                ExpressionValue = sourceElement + "|" + destinationElement,
            };

        }
    }
}
