using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitElementTypeExprTests : BaseTests
    {
        [Test]
        public void WhenElementTypeExpressionIsDefindedThenValidExpresionIsDiscovered()
        {
            // Arrange
            var parser = CreateParserFromText(@"  element.type==Container  ");
            var context = parser.elementTypeExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementTypeExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementType));
            Assert.That(result.ExpressionValue, Is.EqualTo("Container"));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
