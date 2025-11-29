using DocuEye.Structurizr.DSL.Expressions.Model;
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Expressions.Builders.Tests
{
    public class ElementExpressionBuilderTests
    {
        private readonly List<StructurizrModelElement> elements = new List<StructurizrModelElement>()
        {
            new StructurizrModelElement("id0","identifier0")
            {
                Name = "SoftwareSystem",
                Type = StructurizrModelElementType.SoftwareSystem,
                Properties = new Dictionary<string, string>()
                {
                    { "key1", "valueSoftwareSystem1" },
                    { "key2", "value2" }
                },
                Tags = new List<string>() { "tagSoftwareSystem1", "tag2" }
            },
            new StructurizrModelElement("id1","identifier1")
            {
                Name = "Container",
                ParentIdentifier = "identifier0",
                Type = StructurizrModelElementType.Container,
                Technology = "test",
                Properties = new Dictionary<string, string>()
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                },
                Tags = new List<string>() { "tag1", "tag2" }
            },
            new StructurizrModelElement("id2","identifier2")
            {
                Name = "Component",
                Type = StructurizrModelElementType.Component,
                ParentIdentifier = "identifier1",
                Technology = "test2",
                Properties = new Dictionary<string, string>()
                {
                    { "key1", "valueComponent1" },
                    { "key2", "value2" }
                }
                ,
                Tags = new List<string>() { "Component1", "tag2" }
            }
        };

        private readonly List<StructurizrRelationship> relationships = new List<StructurizrRelationship>() { 
            new StructurizrRelationship("id0", new string[] { "tag1" })
            {
                SourceIdentifier = "identifier0",
                DestinationIdentifier = "identifier1",
            },
            new StructurizrRelationship("id1", new string[] { "tag1" })
            {
                SourceIdentifier = "identifier1",
                DestinationIdentifier = "identifier2",
            }
        };

        [Test]
        public void WhenDefineElementTypeExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionOperator = DSLExpressionOperator.Equal,
                    ExpressionType = DSLExpressionType.ElementType,
                    ExpressionConnector = DSLExpressionConnector.Undefined,
                    PropertyName = "name",
                    ExpressionValue = "Container"
                }
            };
            // Act
            var expression = new ElementExpressionBuilder(this.relationships).BuildExpression(dslExpressions);
            var result = elements.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("identifier1"));
        }

        [Test]
        public void WhenDefineElementTechnologyExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionOperator = DSLExpressionOperator.Equal,
                    ExpressionType = DSLExpressionType.ElementTechnology,
                    ExpressionValue = "test"
                }
            };
            // Act
            var expression = new ElementExpressionBuilder(this.relationships).BuildExpression(dslExpressions);
            var result = elements.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("identifier1"));
        }

        [Test]
        public void WhenDefineElementParentExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionOperator = DSLExpressionOperator.Equal,
                    ExpressionType = DSLExpressionType.ElementParent,
                    ExpressionValue = "identifier0"
                }
            };
            // Act
            var expression = new ElementExpressionBuilder(this.relationships).BuildExpression(dslExpressions);
            var result = elements.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("identifier1"));
        }

        [Test]
        public void WhenDefineElementPropertiesExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionOperator = DSLExpressionOperator.Equal,
                    ExpressionType = DSLExpressionType.ElementProperties,
                    PropertyName = "key1",
                    ExpressionValue = "value1"
                }
            };
            // Act
            var expression = new ElementExpressionBuilder(this.relationships).BuildExpression(dslExpressions);
            var result = elements.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("identifier1"));
        }

        [Test]
        public void WhenDefineElementTagsExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionOperator = DSLExpressionOperator.Equal,
                    ExpressionType = DSLExpressionType.ElementTag,
                    ExpressionValue = "tag1, tag2"
                }
            };
            // Act
            var expression = new ElementExpressionBuilder(this.relationships).BuildExpression(dslExpressions);
            var result = elements.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("identifier1"));
        }

        [Test]
        public void WhenDefineElementAfferentCouplingsExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionOperator = DSLExpressionOperator.Equal,
                    ExpressionType = DSLExpressionType.ElementAfferentCouplings,
                    ExpressionValue = "identifier1"
                }
            };
            // Act
            var expression = new ElementExpressionBuilder(this.relationships).BuildExpression(dslExpressions);
            var result = elements.AsQueryable().Where(expression)
                .OrderBy(o => o.Identifier).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result.First().Identifier, Is.EqualTo("identifier0"));
            Assert.That(result.Last().Identifier, Is.EqualTo("identifier1"));
        }

        [Test]
        public void WhenDefineElementEfferentCouplingsExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionOperator = DSLExpressionOperator.Equal,
                    ExpressionType = DSLExpressionType.ElementEfferentCouplings,
                    ExpressionValue = "identifier1"
                }
            };
            // Act
            var expression = new ElementExpressionBuilder(this.relationships).BuildExpression(dslExpressions);
            var result = elements.AsQueryable().Where(expression)
                .OrderBy(o => o.Identifier).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result.First().Identifier, Is.EqualTo("identifier1"));
            Assert.That(result.Last().Identifier, Is.EqualTo("identifier2"));
        }

        [Test]
        public void WhenDefineElementAllCouplingsExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionOperator = DSLExpressionOperator.Equal,
                    ExpressionType = DSLExpressionType.ElementAllCouplings,
                    ExpressionValue = "identifier1"
                }
            };
            // Act
            var expression = new ElementExpressionBuilder(this.relationships).BuildExpression(dslExpressions);
            var result = elements.AsQueryable().Where(expression)
                .OrderBy(o => o.Identifier).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result.First().Identifier, Is.EqualTo("identifier0"));
            Assert.That(result[1].Identifier, Is.EqualTo("identifier1"));
            Assert.That(result.Last().Identifier, Is.EqualTo("identifier2"));
        }
    }
}
