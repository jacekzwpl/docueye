using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitRelationshipSpecifiedExprTests : BaseTests
    {
        [Test]
        public void WhenExpressionIsDefindedThenValidValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@" relationship==sourceid->destid  ");
            var context = parser.relationshipSpecifiedExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitRelationshipSpecifiedExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipSpecified));
            Assert.That(result.ExpressionValue, Is.EqualTo("sourceid|destid"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
