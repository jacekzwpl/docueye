namespace DocuEye.Structurizr.Model.Exploders.Tests.DocsExploding
{
    public class ExplodeDocumentationTests : BaseExploderTests
    {
        [Test]
        public void WhenDocumentationForWorkspaceExistsThenIdIsSetEndElementIdIsNull()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrDocumentation
            {
                
            };
            var exploder = new DocumentationExploder(this.mapper);

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
            var structurizrDocumentation = new StructurizrDocumentation
            {
                
            };
            var exploder = new DocumentationExploder(this.mapper);

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
            var structurizrDocumentation = new StructurizrDocumentation
            {
                Sections = new List<StructurizrDocumentationSection>
                {
                    new StructurizrDocumentationSection
                    {
                        Content = "Content 1",
                        Format = "Markdown",
                        Order = 1,
                    },
                    new StructurizrDocumentationSection
                    {
                        Content = "Content 2",
                        Format = "Markdown",
                        Order = 2,
                    }
                }
            };
            var exploder = new DocumentationExploder(this.mapper);

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(documentation.Sections.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenDocumentationHasNoSectionsThenSectionsAreEmpty()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrDocumentation
            {

            };

            var exploder = new DocumentationExploder(this.mapper);

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(documentation.Sections.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenDocumentationHasDecisionsThenDecisionsAreExploded()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrDocumentation
            {
                Decisions = new List<StructurizrDecision>
                {
                    new StructurizrDecision
                    {
                        Content = "Content 1",
                        Date = "2021-01-01",
                        Id = "1",
                        ElementId = "elementId",
                        Format = "Markdown"
                    },
                    new StructurizrDecision
                    {
                        Content = "Content 2",
                        Date = "2021-01-02",
                        Id = "2",
                        ElementId = "elementId",
                        Format = "Markdown"
                    }
                }
            };
            var exploder = new DocumentationExploder(this.mapper);

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(decisions.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenDocumentationHasNoDecisionsThenDecisionsAreEmpty()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrDocumentation
            {

            };

            var exploder = new DocumentationExploder(this.mapper);

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(decisions.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenDocumentationHasImagesThenImagesAreExploded()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrDocumentation
            {
                Images = new List<StructurizrImage>
                {
                    new StructurizrImage
                    {
                        Content = "Content 1",
                        Name = "Name 1",
                        Type = "Type 1",
                    },
                    new StructurizrImage
                    {
                        Content = "Content 2",
                        Name = "Name 2",
                        Type = "Type 2",
                    }
                }
            };
            var exploder = new DocumentationExploder(this.mapper);

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(images.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenDocumentationHasNoImagesThenImagesAreEmpty()
        {
            // Arrange
            var structurizrDocumentation = new StructurizrDocumentation
            {

            };
            var exploder = new DocumentationExploder(this.mapper);

            // Act
            var (documentation,decisions,images) = exploder.ExplodeDocumentation(structurizrDocumentation);

            // Assert
            Assert.That(images.Count(), Is.EqualTo(0));
        }


    }
}
