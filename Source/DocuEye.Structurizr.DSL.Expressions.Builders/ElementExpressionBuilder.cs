using DocuEye.Structurizr.DSL.Expressions.Model;
using DocuEye.Structurizr.DSL.Model;
using System.Linq.Expressions;

namespace DocuEye.Structurizr.DSL.Expressions.Builders
{
    public class ElementExpressionBuilder
    {
        private readonly List<StructurizrRelationship> relationships;

        public ElementExpressionBuilder(List<StructurizrRelationship> relationships)
        {
            this.relationships = relationships;
        }

        public bool AreAnyElementExpression(IEnumerable<DSLExpression> dslExpressions)
        {
            return dslExpressions.Any(dslExpression => dslExpression.ExpressionType == DSLExpressionType.ElementType ||
                                                    dslExpression.ExpressionType == DSLExpressionType.ElementTechnology ||
                                                    dslExpression.ExpressionType == DSLExpressionType.ElementParent ||
                                                    dslExpression.ExpressionType == DSLExpressionType.ElementProperties ||
                                                    dslExpression.ExpressionType == DSLExpressionType.ElementTag ||
                                                    dslExpression.ExpressionType == DSLExpressionType.ElementAfferentCouplings ||
                                                    dslExpression.ExpressionType == DSLExpressionType.ElementEfferentCouplings ||
                                                    dslExpression.ExpressionType == DSLExpressionType.ElementAllCouplings);
        }
        
        
        public Expression<Func<StructurizrModelElement, bool>> BuildExpression(IEnumerable<DSLExpression> dslExpressions)
        {
            Expression<Func<StructurizrModelElement, bool>>? expression = null ;
            foreach (var dslExpression in dslExpressions)
            { 
                if (expression == null)
                {
                    var currentExpression = this.BuildExpression(dslExpression);
                    if(currentExpression != null) 
                    { 
                        expression = currentExpression; 
                    }
                }else if(dslExpression.ExpressionConnector == DSLExpressionConnector.Or || dslExpression.ExpressionConnector == DSLExpressionConnector.Undefined)
                {
                    var currentExpression = this.BuildExpression(dslExpression);
                    if (currentExpression != null)
                    {
                        expression = expression.Or(currentExpression);
                    }
                }
                else if (dslExpression.ExpressionConnector == DSLExpressionConnector.And)
                {
                    var currentExpression = this.BuildExpression(dslExpression);
                    if (currentExpression != null)
                    {
                        expression = expression.And(currentExpression);
                    }

                   
                }
            }
            if(expression == null)
            {
                return ExpressionBuilder.New<StructurizrModelElement>(o => true);
            }
            return expression;
        }


        public Expression<Func<StructurizrModelElement, bool>>? BuildExpression(DSLExpression dslExpression)
        {
            if(string.IsNullOrEmpty(dslExpression.ExpressionValue))
            {
                return null;
            }
            switch(dslExpression.ExpressionType)
            {
                case DSLExpressionType.ElementType:
                    return this.BuildElementTypeExpression(dslExpression);
                case DSLExpressionType.ElementTechnology:
                    return ExpressionBuilder.New<StructurizrModelElement>(o => o.Technology == dslExpression.ExpressionValue);
                case DSLExpressionType.ElementParent:
                    return ExpressionBuilder.New<StructurizrModelElement>(o => o.ParentIdentifier == dslExpression.ExpressionValue);
                case DSLExpressionType.ElementProperties:
                    return ExpressionBuilder.New<StructurizrModelElement>(o => o.Properties.ContainsKey(dslExpression.PropertyName) && o.Properties[dslExpression.PropertyName] == dslExpression.ExpressionValue);
                case DSLExpressionType.ElementTag:
                    var requiredTags = dslExpression.ExpressionValue.Split(',').Select(tag => tag.Trim()).ToList();
                    return ExpressionBuilder.New<StructurizrModelElement>(o => requiredTags.All(tag => o.Tags.Contains(tag)));
                case DSLExpressionType.ElementAfferentCouplings:
                    return this.ElementAfferentCouplingsExpression(dslExpression);
                case DSLExpressionType.ElementEfferentCouplings:
                    return this.ElementEfferentCouplingsExpression(dslExpression);
                case DSLExpressionType.ElementAllCouplings:
                    return this.ElementAllCouplingsExpression(dslExpression);
                default:
                    return null;
            }
        }

        private Expression<Func<StructurizrModelElement, bool>>? ElementAllCouplingsExpression(DSLExpression dslExpression)
        {
            if (string.IsNullOrEmpty(dslExpression.ExpressionValue))
            {
                return null;
            }
            var efferentElementIds = this.relationships.AsQueryable().Where(rel => rel.SourceIdentifier == dslExpression.ExpressionValue)
                .Select(rel => rel.DestinationIdentifier).Distinct().ToArray();
            var afferentElementIds = this.relationships.AsQueryable().Where(rel => rel.DestinationIdentifier == dslExpression.ExpressionValue)
                .Select(rel => rel.SourceIdentifier).Distinct().ToArray();
            var elementIds = new List<string>();
            elementIds.AddRange(efferentElementIds);
            elementIds.AddRange(afferentElementIds);
            elementIds.Add(dslExpression.ExpressionValue);
            return ExpressionBuilder.New<StructurizrModelElement>(o => elementIds.Contains(o.Identifier));
        }

        private Expression<Func<StructurizrModelElement, bool>>? ElementEfferentCouplingsExpression(DSLExpression dslExpression)
        {
            if (string.IsNullOrEmpty(dslExpression.ExpressionValue))
            {
                return null;
            }
            var elementIds = this.relationships.AsQueryable().Where(rel => rel.SourceIdentifier == dslExpression.ExpressionValue)
                .Select(rel => rel.DestinationIdentifier).Distinct().ToList();
            elementIds.Add(dslExpression.ExpressionValue);
            return ExpressionBuilder.New<StructurizrModelElement>(o => elementIds.Contains(o.Identifier));
        }

        private Expression<Func<StructurizrModelElement, bool>>? ElementAfferentCouplingsExpression(DSLExpression dslExpression)
        {
            if(string.IsNullOrEmpty(dslExpression.ExpressionValue))
            {
                return null;
            }
            var elementIds = this.relationships.AsQueryable().Where(rel => rel.DestinationIdentifier == dslExpression.ExpressionValue)
                .Select(rel => rel.SourceIdentifier).Distinct().ToList();
            elementIds.Add(dslExpression.ExpressionValue);
            return ExpressionBuilder.New<StructurizrModelElement>(o => elementIds.Contains(o.Identifier));
        }


        private Expression<Func<StructurizrModelElement, bool>>? BuildElementTypeExpression(DSLExpression dslExpression)
        {
            var value = dslExpression.ExpressionValue == "Custom" ? "CustomElement" : dslExpression.ExpressionValue;
            StructurizrModelElementType typeValue;
            if (Enum.TryParse<StructurizrModelElementType>(dslExpression.ExpressionValue, true, out typeValue))
            {
                return ExpressionBuilder.New<StructurizrModelElement>(o => o.Type == typeValue);
            }

            return null;
        }

    }
}
