using DocuEye.Structurizr.Json.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.DocsExploding
{
    public class ExplodeDocumentationTests : BaseExploderTests
    {
        [Test]
        public void WhenDocumentationForWorkspaceExistsThenIdIsSetEndElementIdIsNull()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrJsonDocumentation
            {
                
            };
            var exploder = new DocumentationExploder();

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(documentation.Id, Is.Not.Null);
            Assert.That(documentation.StructurizrElementId, Is.Null);
        }

        [Test]
        public void WhenDocumentationForElementExistsThenIdIsSetEndElementIdIsSet()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrJsonDocumentation
            {
                
            };
            var exploder = new DocumentationExploder();

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation, "elementId");

            // Assert
            Assert.That(documentation.Id, Is.Not.Null);
            Assert.That(documentation.StructurizrElementId, Is.EqualTo("elementId"));
        }

        [Test]
        public void WhenDocumentationHasSectionsThenSectionsAreExploded()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrJsonDocumentation
            {
                Sections = new List<StructurizrJsonDocumentationSection>
                {
                    new StructurizrJsonDocumentationSection
                    {
                        Content = "Content 1",
                        Format = "Markdown",
                        Order = 1,
                    },
                    new StructurizrJsonDocumentationSection
                    {
                        Content = "Content 2",
                        Format = "Markdown",
                        Order = 2,
                    }
                }
            };
            var exploder = new DocumentationExploder();

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(documentation.Sections.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenDocumentationHasNoSectionsThenSectionsAreEmpty()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrJsonDocumentation
            {

            };

            var exploder = new DocumentationExploder();

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(documentation.Sections.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenDocumentationHasDecisionsThenDecisionsAreExploded()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrJsonDocumentation
            {
                Decisions = new List<StructurizrJsonDecision>
                {
                    new StructurizrJsonDecision
                    {
                        Content = "Content 1",
                        Date = "2021-01-01",
                        Id = "1",
                        ElementId = "elementId",
                        Format = "Markdown"
                    },
                    new StructurizrJsonDecision
                    {
                        Content = "Content 2",
                        Date = "2021-01-02",
                        Id = "2",
                        ElementId = "elementId",
                        Format = "Markdown"
                    }
                }
            };
            var exploder = new DocumentationExploder();

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(decisions.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenDocumentationHasNoDecisionsThenDecisionsAreEmpty()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrJsonDocumentation
            {

            };

            var exploder = new DocumentationExploder();

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(decisions.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenDocumentationHasImagesThenImagesAreExploded()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrJsonDocumentation
            {
                Images = new List<StructurizrJsonImage>
                {
                    new StructurizrJsonImage
                    {
                        Content = "Content 1",
                        Name = "Name 1",
                        Type = "Type 1",
                    },
                    new StructurizrJsonImage
                    {
                        Content = "Content 2",
                        Name = "Name 2",
                        Type = "Type 2",
                    }
                }
            };
            var exploder = new DocumentationExploder();

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(images.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenDocumentationHasNoImagesThenImagesAreEmpty()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrJsonDocumentation
            {

            };
            var exploder = new DocumentationExploder();

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(images.Count(), Is.EqualTo(0));
        }


    }
}
