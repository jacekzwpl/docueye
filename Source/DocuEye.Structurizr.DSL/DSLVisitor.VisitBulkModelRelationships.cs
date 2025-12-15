using Antlr4.Runtime.Misc;
using DocuEye.Structurizr.DSL.Expressions;
using DocuEye.Structurizr.DSL.Expressions.Builders;
using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL
{
    public partial class DSLVisitor
    {
        public override List<Object> VisitBulkModelRelationships([NotNull] BulkModelRelationshipsContext context)
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
            var relationshipsExpressionBuilder = new RelationshipExpressionBuilder();
            var relationshipsExpression = relationshipsExpressionBuilder.BuildExpression(expressions);
            var relationships = this.workspace.Model.Relationships.AsQueryable()
                .Where(relationshipsExpression).ToArray();
            var bodyContext = context.modelElementBody();
            if (bodyContext != null) {
                foreach (var relationship in relationships)
                {
                    var modelRelationship = this.VisitModelElementBody(bodyContext, relationship.ToModelElement());
                    relationship.FromModelElement(modelRelationship);
                }
            }
            
            return new List<Object>();

        }
    }
}
