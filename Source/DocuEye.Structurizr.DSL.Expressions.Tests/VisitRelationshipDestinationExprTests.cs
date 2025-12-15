using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitRelationshipDestinationExprTests : BaseTests
    {
        [Test]
        public void WhenRelationshipSourceKeywordIsUsedThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship.destination==elementid  ");
            var context = parser.relationshipDestinationExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipDestinationExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipDestination));
            Assert.That(result.ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenRelationshipKeywordIsUsedThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship==*->elementid  ");
            var context = parser.relationshipDestinationExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipDestinationExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipDestination));
            Assert.That(result.ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenNoKeywordIsUsedThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  *->elementid  ");
            var context = parser.relationshipDestinationExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipDestinationExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipDestination));
            Assert.That(result.ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
