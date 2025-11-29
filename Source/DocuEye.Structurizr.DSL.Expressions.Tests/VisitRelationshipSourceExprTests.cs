using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitRelationshipSourceExprTests : BaseTests
    {
        [Test]
        public void WhenRelationshipSourceKeywordIsUsedThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship.source==elementid  ");
            var context = parser.relationshipSourceExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipSourceExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipSource));
            Assert.That(result.ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenRelationshipKeywordIsUsedThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship==elementid->*  ");
            var context = parser.relationshipSourceExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipSourceExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipSource));
            Assert.That(result.ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenNoKeywordIsUsedThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  elementid->*  ");
            var context = parser.relationshipSourceExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipSourceExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipSource));
            Assert.That(result.ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
