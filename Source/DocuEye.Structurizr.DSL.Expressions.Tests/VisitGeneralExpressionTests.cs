using DocuEye.Structurizr.DSL.Expressions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Tests
{
    public class VisitGeneralExpressionTests : BaseTests
    {
        [Test]
        public void WhenElementExpressionsAreDefinedThenAllExistInResult()
        {
            // Arrange
            var parser = CreateParserFromText(@"element.type==Container || element.technology == Custom && element.tag == tag1, tag2 && element.parent==elementid && element.properties[propname]==testpropertyvalue && ->elementid->");
            var context = parser.generalExpression();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitGeneralExpression(context);
            // Assert
            Assert.That(result.Count(), Is.EqualTo(6));
            Assert.That(result.ElementAt(0).ExpressionType, Is.EqualTo(DSLExpressionType.ElementType));
            Assert.That(result.ElementAt(0).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(0).ExpressionValue, Is.EqualTo("Container"));
            Assert.That(result.ElementAt(0).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.Undefined));
            Assert.That(result.ElementAt(1).ExpressionType, Is.EqualTo(DSLExpressionType.ElementTechnology));
            Assert.That(result.ElementAt(1).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(1).ExpressionValue, Is.EqualTo("Custom"));
            Assert.That(result.ElementAt(1).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.Or));
            Assert.That(result.ElementAt(2).ExpressionType, Is.EqualTo(DSLExpressionType.ElementTag));
            Assert.That(result.ElementAt(2).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(2).ExpressionValue, Is.EqualTo("tag1, tag2"));
            Assert.That(result.ElementAt(2).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(result.ElementAt(3).ExpressionType, Is.EqualTo(DSLExpressionType.ElementParent));
            Assert.That(result.ElementAt(3).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(3).ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ElementAt(3).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(result.ElementAt(4).ExpressionType, Is.EqualTo(DSLExpressionType.ElementProperties));
            Assert.That(result.ElementAt(4).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(4).PropertyName, Is.EqualTo("propname"));
            Assert.That(result.ElementAt(4).ExpressionValue, Is.EqualTo("testpropertyvalue"));
            Assert.That(result.ElementAt(4).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(result.ElementAt(5).ExpressionType, Is.EqualTo(DSLExpressionType.ElementAllCouplings));
            Assert.That(result.ElementAt(5).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(5).ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ElementAt(5).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }

        [Test]
        public void WhenRelationshipExpressionsAreDefinedThenAllExistInResult()
        {
            // Arrange
            var parser = CreateParserFromText(@"relationship.tag==tag1, tag2 && relationship.properties[propname]==testpropertyvalue && *->elementid && elementid->* && *->* && relationship==sourceElement->destinationElement");
            var context = parser.generalExpression();
            var visitor = new DSLExpressionsVisitor();
            // Act
            var result = visitor.VisitGeneralExpression(context);
            // Assert
            Assert.That(result.Count(), Is.EqualTo(6));
            Assert.That(result.ElementAt(0).ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipTag));
            Assert.That(result.ElementAt(0).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(0).ExpressionValue, Is.EqualTo("tag1, tag2"));
            Assert.That(result.ElementAt(0).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.Undefined));
            Assert.That(result.ElementAt(1).ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipProperties));
            Assert.That(result.ElementAt(1).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(1).PropertyName, Is.EqualTo("propname"));
            Assert.That(result.ElementAt(1).ExpressionValue, Is.EqualTo("testpropertyvalue"));
            Assert.That(result.ElementAt(1).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(result.ElementAt(2).ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipDestination));
            Assert.That(result.ElementAt(2).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(2).ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ElementAt(2).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(result.ElementAt(3).ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipSource));
            Assert.That(result.ElementAt(3).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(3).ExpressionValue, Is.EqualTo("elementid"));
            Assert.That(result.ElementAt(3).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(result.ElementAt(4).ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipAll));
            Assert.That(result.ElementAt(4).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(result.ElementAt(5).ExpressionType, Is.EqualTo(DSLExpressionType.RelationshipSpecified));
            Assert.That(result.ElementAt(5).ExpressionOperator, Is.EqualTo(DSLExpressionOperator.Equal));
            Assert.That(result.ElementAt(5).ExpressionValue, Is.EqualTo("sourceElement|destinationElement"));
            Assert.That(result.ElementAt(5).ExpressionConnector, Is.EqualTo(DSLExpressionConnector.And));
            Assert.That(parser.NumberOfSyntaxErrors, Is.EqualTo(0));
        }
    }
}
