namespace DocuEye.Structurizr.DslToJson.Tests
{
    public class DocumentationReaderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenReadDocumentationThenAllPropertiesArCorrect()
        {
            // Arrange
            var reader = new DocumentationReader(Directory.GetCurrentDirectory());
            // Act
            var documentation = reader.Read("TestFiles/doc", "TestFiles/adr");
            // Assert
            Assert.That(documentation.Sections?.Count(), Is.EqualTo(2));
            Assert.That(documentation.Decisions?.Count(), Is.EqualTo(2));
            Assert.That(documentation.Images?.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenDiscoverImageThenAllPropertiesAreCorrect()
        {
            // Arrange
            var reader = new DocumentationReader(Directory.GetCurrentDirectory());
            // Act
            var images = reader.DiscoverImage("TestFiles/doc", "![alt text](images/test-diagram.png)");
            // Assert
            Assert.That(images.Count(), Is.EqualTo(1));
            Assert.That(images.First().Name, Is.EqualTo("images/test-diagram.png"));
            Assert.That(images.First().Type, Is.EqualTo("image/png"));
        }

        [Test]
        public void WhenReadSingleDocumentationFileThenAllPropertiesAreCorrect()
        {
            // Arrange
            var reader = new DocumentationReader(Directory.GetCurrentDirectory());
            // Act
            var section = reader.ReadSection("TestFiles/doc/0001-expampledocumentation1.md");
            // Assert
            Assert.That(section.Content?.Length, Is.GreaterThan(1));
        }

        [Test]
        public void WhenReadMultipleDocumentationFilesThenAllPropertiesAreCorrect()
        {
            // Arrange
            var reader = new DocumentationReader(Directory.GetCurrentDirectory());
            // Act
            var sections = reader.ReadSections("TestFiles/doc");
            // Assert
            Assert.That(sections.Count(), Is.EqualTo(2));
            Assert.That(sections.First().Content?.Length, Is.GreaterThan(1));
            Assert.That(sections.Last().Content?.Length, Is.GreaterThan(1));
        }

        [Test]
        public void WhenReadSingleDecisionThenAllPropertiesAreCorrect()
        {
            // Arrange
            var reader = new DocumentationReader(Directory.GetCurrentDirectory());
            // Act
            var decision = reader.ReadDecision("TestFiles/adr/0001-test-decision1.md");
            // Assert
            Assert.That(decision.Title, Is.EqualTo("First important decision about something"));
            Assert.That(decision.Date, Is.EqualTo("2027-01-24T00:00:00Z"));
            Assert.That(decision.Status, Is.EqualTo("Accepted"));
            Assert.That(decision.Content?.Length, Is.GreaterThan(1));
        }

        [Test]
        public void WhenReadMultipleDecisionsThenAllPropertiesAreCorrect()
        {
            // Arrange
            var reader = new DocumentationReader(Directory.GetCurrentDirectory());
            // Act
            var decisions = reader.ReadDecisions("TestFiles/adr");
            // Assert
            Assert.That(decisions.Count(), Is.EqualTo(2));
            //Assert.That(decisions.First().Title, Is.EqualTo("First important decision about something"));
            //Assert.That(decisions.Last().Title, Is.EqualTo("Second important decision about something"));
        }
    }
}