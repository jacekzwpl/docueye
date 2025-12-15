using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLExpressionsParser;

namespace DocuEye.Structurizr.DSL.Expressions
{
    public partial class DSLExpressionsVisitor
    {

        public override DSLExpression VisitElementTypeExpr([NotNull] ElementTypeExprContext context)
        {
            var value = context.expressionValue()?.GetText().Trim();
            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.ElementType,
                ExpressionValue = value
            };
        }

        public override DSLExpression VisitElementParentExpr([NotNull] ElementParentExprContext context)
        {
            var value = context.expressionValue()?.GetText().Trim();
            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.ElementParent,
                ExpressionValue = value
            };
        }

        public override DSLExpression VisitElementTagExpr([NotNull] ElementTagExprContext context)
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
                ExpressionType = DSLExpressionType.ElementTag,
                ExpressionValue = value.Trim(),
                ExpressionOperator = context.EQUALS() != null ? DSLExpressionOperator.Equal : DSLExpressionOperator.NotEqual
            };
        }

        public override DSLExpression VisitElementTechnologyExpr([NotNull] ElementTechnologyExprContext context)
        {
            var value = context.expressionValue()?.GetText().Trim();
            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.ElementTechnology,
                ExpressionValue = value,
                ExpressionOperator = context.EQUALS() != null ? DSLExpressionOperator.Equal : DSLExpressionOperator.NotEqual
            };
        }

        public override DSLExpression VisitElementPropertiesExpr([NotNull] ElementPropertiesExprContext context)
        {
            var value = context.expressionValue()?.GetText().Trim();
            var name = context.propertyName()?.GetText().Trim();
            return new DSLExpression()
            {
                ExpressionType = DSLExpressionType.ElementProperties,
                PropertyName = name,
                ExpressionValue = value,
                ExpressionOperator = context.EQUALS() != null ? DSLExpressionOperator.Equal : DSLExpressionOperator.NotEqual
            };
        }

        public override DSLExpression VisitElementWithCouplingsExpr([NotNull] ElementWithCouplingsExprContext context)
        {

            if (context.children == null)
            {
                throw new Exception("Expression is invalid");
            }

            var value = string.Empty;
            var hasAfferentCouplings = false;
            var hasEfferentCouplings = false;

            foreach (var childContext in context.children)
            {
                if (childContext is RelationshipPointerContext)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        hasAfferentCouplings = true;
                    }
                    else
                    {
                        hasEfferentCouplings = true;
                    }
                }

                if (childContext is ExpressionValueContext)
                {
                    value = childContext.GetText().Trim();
                }
            }

            if (context.MORE_THAN() != null && value.EndsWith("-"))
            {
                value = value.Substring(0, value.Length - 1);
                hasEfferentCouplings = true;
            }

            return new DSLExpression()
            {
                ExpressionType =
                    hasAfferentCouplings && hasEfferentCouplings ? DSLExpressionType.ElementAllCouplings :
                    hasAfferentCouplings && !hasEfferentCouplings ? DSLExpressionType.ElementAfferentCouplings :
                    DSLExpressionType.ElementEfferentCouplings,
                ExpressionValue = value,
                ExpressionOperator = DSLExpressionOperator.Equal,
            };

        }
    }
}
