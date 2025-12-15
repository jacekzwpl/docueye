using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitRelationshipAllExprTests : BaseTests
    {
        [Test] 
        public void WhenExpressionIsDefindedUsingShortcutThenValidValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"  *->*  ");
            var context = parser.relationshipAllExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipAllExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipAll));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenExpressionIsDefindedUsingKeywordThenValidValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@" relationship==*  ");
            var context = parser.relationshipAllExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipAllExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipAll));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenExpressionIsDefindedUsingKeywordAndShortcutThenValidValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"  relationship==*->*  ");
            var context = parser.relationshipAllExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipAllExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipAll));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
