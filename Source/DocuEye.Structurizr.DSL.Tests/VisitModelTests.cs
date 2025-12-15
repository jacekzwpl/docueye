namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitModelTests : BaseTests
    {
        [Test]
        public void WhenModelIsDefinedThenModelIsNotNull()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            model 
            ");
            var modelContext = parser.model();
            var visitor = new DSLVisitor();

            // Act
            var model = visitor.VisitModel(modelContext);

            // Assert
            Assert.That(model, Is.Not.Null);
        }
    }
}
