using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitElementTechnologyExprTests : BaseTests
    {
        [Test]
        public void WhenExpressionOperatorIsEqualsThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  element.technology==technologyname  ");
            var context = parser.elementTechnologyExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementTechnologyExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementTechnology));
            Assert.That(result.ExpressionValue, Is.EqualTo("technologyname"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
        [Test]
        public void WhenExpressionOperatorIsNotEqualsThenExpressionHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  element.technology!=technologyname  ");
            var context = parser.elementTechnologyExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementTechnologyExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementTechnology));
            Assert.That(result.ExpressionValue, Is.EqualTo("technologyname"));
            Assert.That(result.ExpressionOperator, Is.EqualTo(DSLExpressionOperator.NotEqual));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
