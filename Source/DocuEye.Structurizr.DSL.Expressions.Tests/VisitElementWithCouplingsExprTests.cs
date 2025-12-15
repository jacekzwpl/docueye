using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitElementWithCouplingsExprTests : BaseTests
    {
        [Test]
        public void WhenAfferentCouplingsExpressionIsDefindedUsingElementKeywordThenAllPropoertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  element==->myelementid  ");
            var context = parser.elementWithCouplingsExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementWithCouplingsExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementAfferentCouplings));
            Assert.That(result.ExpressionValue, Is.EqualTo("myelementid"));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenAfferentCouplingsExpressionIsDefindedNotUsingElementKeywordThenAllPropoertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  ->myelementid  ");
            var context = parser.elementWithCouplingsExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementWithCouplingsExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementAfferentCouplings));
            Assert.That(result.ExpressionValue, Is.EqualTo("myelementid"));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenEfferentCouplingsExpressionIsDefindedUsingElementKeywordThenAllPropoertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  element==myelementid->  ");
            var context = parser.elementWithCouplingsExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementWithCouplingsExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementEfferentCouplings));
            Assert.That(result.ExpressionValue, Is.EqualTo("myelementid"));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenEfferentCouplingsExpressionIsDefindedNotUsingElementKeywordThenAllPropoertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  myelementid->  ");
            var context = parser.elementWithCouplingsExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementWithCouplingsExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementEfferentCouplings));
            Assert.That(result.ExpressionValue, Is.EqualTo("myelementid"));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenAllCouplingsExpressionIsDefindedUsingElementKeywordThenAllPropoertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  element==->myelementid ->  ");
            var context = parser.elementWithCouplingsExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementWithCouplingsExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementAllCouplings));
            Assert.That(result.ExpressionValue, Is.EqualTo("myelementid"));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenAllCouplingsExpressionIsDefindedNotUsingElementKeywordThenAllPropoertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"  ->myelementid->  ");
            var context = parser.elementWithCouplingsExpr();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitElementWithCouplingsExpr(context);
            // Assert
            Assert.That(result, !Is.Null);
            Assert.That(result.ExpressionType, Is.EqualTo(DSLExpressionType.ElementAllCouplings));
            Assert.That(result.ExpressionValue, Is.EqualTo("myelementid"));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

    }
}
