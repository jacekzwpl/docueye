using DocuEye.Structurizr.DSL.Expressions.Model;
using DocuEye.Structurizr.DSL.Model;

namespace DocuEye.Structurizr.DSL.Expressions.Builders.Tests
{
    public class RelationshipExpressionBuilderTests
    {
        private readonly List<StructurizrRelationship> relationships = new List<StructurizrRelationship>() {
            new StructurizrRelationship("id0", new string[] { "tag1" })
            {
                SourceIdentifier = "identifier0",
                DestinationIdentifier = "identifier1",
                Properties = new Dictionary<string, string>()
                {
                    { "property1", "value1" },
                    { "property2", "value2" }
                }
            },
            new StructurizrRelationship("id1", new string[] { "tag2" })
            {
                SourceIdentifier = "identifier1",
                DestinationIdentifier = "identifier2",
            }
        };

        [Test]
        public void WhenDefineRelationshipTagExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionType = DSLExpressionType.RelationshipTag,
                    ExpressionValue = "tag1"
                }
            };
            // Act
            var expression = new RelationshipExpressionBuilder().BuildExpression(dslExpressions);
            var result = relationships.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("id0"));
        }

        [Test]
        public void WhenDefineRelationshipTagExpressionWithNegationThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionType = DSLExpressionType.RelationshipTag,
                    ExpressionValue = "tag1",
                    ExpressionOperator = DSLExpressionOperator.NotEqual
                }
            };
            // Act
            var expression = new RelationshipExpressionBuilder().BuildExpression(dslExpressions);
            var result = relationships.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("id1"));
        }

        [Test]
        public void WhenDefineRelationshipPropertiesExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionType = DSLExpressionType.RelationshipProperties,
                    ExpressionValue = "value1",
                    PropertyName = "property1"

                }
            };
            // Act
            var expression = new RelationshipExpressionBuilder().BuildExpression(dslExpressions);
            var result = relationships.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("id0"));
        }

        [Test]
        public void WhenDefineRelationshipSourceExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionType = DSLExpressionType.RelationshipSource,
                    ExpressionValue = "identifier0"
                }
            };
            // Act
            var expression = new RelationshipExpressionBuilder().BuildExpression(dslExpressions);
            var result = relationships.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("id0"));
        }


        [Test]
        public void WhenDefineRelationshipDestinationExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionType = DSLExpressionType.RelationshipDestination,
                    ExpressionValue = "identifier1"

                }
            };
            // Act
            var expression = new RelationshipExpressionBuilder().BuildExpression(dslExpressions);
            var result = relationships.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("id0"));
        }

        [Test]
        public void WhenDefineRelationshipAllExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionType = DSLExpressionType.RelationshipAll,
                    ExpressionValue = "*->*"
                }
            };
            // Act
            var expression = new RelationshipExpressionBuilder().BuildExpression(dslExpressions);
            var result = relationships.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(2));
        }


        [Test]
        public void WhenDefineRelationshipSpecifiedExpressionThenFilteringGivesGoodResult()
        {
            // Arrange
            List<DSLExpression> dslExpressions = new List<DSLExpression>()
            {
                new DSLExpression()
                {
                    ExpressionType = DSLExpressionType.RelationshipSpecified,
                    ExpressionValue = "identifier0|identifier1"

                }
            };
            // Act
            var expression = new RelationshipExpressionBuilder().BuildExpression(dslExpressions);
            var result = relationships.AsQueryable().Where(expression).ToArray();
            // Assert
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.First().Identifier, Is.EqualTo("id0"));
        }
    }
}
