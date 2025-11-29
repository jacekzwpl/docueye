using DocuEye.Structurizr.DSL.Expressions.Model;
using DocuEye.Structurizr.DSL.Model;
using System.Linq.Expressions;

namespace DocuEye.Structurizr.DSL.Expressions.Builders
{
    public class RelationshipExpressionBuilder
    {

        public bool AreAnyRelationshipExpressions(IEnumerable<DSLExpression> dslExpressions)
        {
            return dslExpressions.Any(dslExpression => dslExpression.ExpressionType == DSLExpressionType.RelationshipTag ||
                                                    dslExpression.ExpressionType == DSLExpressionType.RelationshipSource ||
                                                    dslExpression.ExpressionType == DSLExpressionType.RelationshipDestination ||
                                                    dslExpression.ExpressionType == DSLExpressionType.RelationshipSpecified ||
                                                    dslExpression.ExpressionType == DSLExpressionType.RelationshipAll ||
                                                    dslExpression.ExpressionType == DSLExpressionType.RelationshipProperties);
        }

        public Expression<Func<StructurizrRelationship, bool>> BuildExpression(IEnumerable<DSLExpression> dslExpressions)
        {
            Expression<Func<StructurizrRelationship, bool>>? expression = null;
            foreach (var dslExpression in dslExpressions)
            {
                if (expression == null)
                {
                    var currentExpression = this.BuildExpression(dslExpression);
                    if (currentExpression != null)
                    {
                        expression = currentExpression;
                    }
                }
                else if (dslExpression.ExpressionConnector == DSLExpressionConnector.And || dslExpression.ExpressionConnector == DSLExpressionConnector.Undefined)
                {
                    var currentExpression = this.BuildExpression(dslExpression);
                    if (currentExpression != null)
                    {
                        expression = expression.And(currentExpression);
                    }
                }
                else if (dslExpression.ExpressionConnector == DSLExpressionConnector.Or)
                {
                    var currentExpression = this.BuildExpression(dslExpression);
                    if (currentExpression != null)
                    {
                        expression = expression.Or(currentExpression);
                    }
                }
            }
            if(expression == null)
            {
                return ExpressionBuilder.New<StructurizrRelationship>(o => true);
            }
            return expression;
        }


        public Expression<Func<StructurizrRelationship, bool>>? BuildExpression(DSLExpression dslExpression)
        {
            if(string.IsNullOrEmpty(dslExpression.ExpressionValue))
            {
                return null;
            }
            switch (dslExpression.ExpressionType)
            {
                case DSLExpressionType.RelationshipTag:
                    var requiredTags = dslExpression.ExpressionValue.Split(',').Select(tag => tag.Trim()).ToList();
                    if(dslExpression.ExpressionOperator == DSLExpressionOperator.NotEqual)
                    {
                        return ExpressionBuilder.New<StructurizrRelationship>(o => requiredTags.Any(tag => !o.Tags.Contains(tag)));
                    }else
                    {
                        return ExpressionBuilder.New<StructurizrRelationship>(o => requiredTags.All(tag => o.Tags.Contains(tag)));
                    }
                        
                case DSLExpressionType.RelationshipSource:
                    return ExpressionBuilder.New<StructurizrRelationship>(o => o.SourceIdentifier == dslExpression.ExpressionValue);
                case DSLExpressionType.RelationshipDestination:
                    return ExpressionBuilder.New<StructurizrRelationship>(o => o.DestinationIdentifier == dslExpression.ExpressionValue);
                case DSLExpressionType.RelationshipSpecified:
                    var sourceAndDestination = dslExpression.ExpressionValue.Split('|').ToList();
                    if (sourceAndDestination.Count() != 2)
                    {
                        return null;
                    }
                    return ExpressionBuilder.New<StructurizrRelationship>(o => o.SourceIdentifier == sourceAndDestination.First() && o.DestinationIdentifier == sourceAndDestination.Last());
                case DSLExpressionType.RelationshipAll:
                    return ExpressionBuilder.New<StructurizrRelationship>(o => true);
                case DSLExpressionType.RelationshipProperties:
                    if (string.IsNullOrEmpty(dslExpression.PropertyName))
                    {
                        return null;
                    }
                    return ExpressionBuilder.New<StructurizrRelationship>(o => o.Properties.ContainsKey(dslExpression.PropertyName) && o.Properties[dslExpression.PropertyName] == dslExpression.ExpressionValue);
                default:
                    return null;
            }
        }
    }
}
