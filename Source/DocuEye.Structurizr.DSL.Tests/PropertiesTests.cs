namespace DocuEye.Structurizr.DSL.Tests
{
    public class PropertiesTests : BaseTests
    {
        [Test]
        public void WhenPropertiesAreSetThenPropertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            ");
            var context = parser.properties();
            var visitor = new DSLVisitor();

            // Act
            Dictionary<string,string> result = (Dictionary<string, string>)visitor.VisitProperties(context);

            // Assert
            Assert.That(result.ContainsKey("key1"), Is.EqualTo(true));
            Assert.That(result.ContainsKey("key2"), Is.EqualTo(true));
            Assert.That(result["key1"], Is.EqualTo("value1"));
            Assert.That(result["key2"], Is.EqualTo("value2"));
        }

        [Test]
        public void WhenDuplicateKeyIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties {
                ""key1"" ""value1""
                ""key1"" ""value2""
            }
            ");
            var context = parser.properties();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitProperties(context));
        }
    }
}
