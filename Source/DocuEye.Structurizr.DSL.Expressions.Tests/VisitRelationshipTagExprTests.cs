using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitRelationshipTagExprTests : BaseTests
    {
        [Test]
        public void WhenExpressionOperatorIsEqualsThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship.tag==tagname  ");
            var context = parser.relationshipTagExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipTagExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipTag));
            Assert.That(result.ExpressionValue, Is.EqualTo("tagname"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenExpressionOperatorIsNotEqualsThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship.tag!=tagname  ");
            var context = parser.relationshipTagExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipTagExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipTag));
            Assert.That(result.ExpressionValue, Is.EqualTo("tagname"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.NotEqual));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenMultipleTagsAreDefinedThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship.tag==tagname,tagname2  ");
            var context = parser.relationshipTagExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipTagExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipTag));
            Assert.That(result.ExpressionValue, Is.EqualTo("tagname,tagname2"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenExpressionValueContainSpacesThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship.tag==tag name  ");
            var context = parser.relationshipTagExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipTagExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipTag));
            Assert.That(result.ExpressionValue, Is.EqualTo("tag name"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
