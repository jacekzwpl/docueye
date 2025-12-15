using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocuEye.Structurizr.DSL.Expressions.Model;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public  class VisitElementParentExprTests : BaseTests
    {
        [Test]
        public void WhenElementParentExpressionIsDefindedThenValidExpresionIsDiscovered()
        {
            // Arrange
            var parser = CreateParserFromText(@"  element.parent==systemname.containername  ");
            var context = parser.elementParentExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementParentExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementParent));
            Assert.That(result.ExpressionValue, Is.EqualTo("systemname.containername"));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
