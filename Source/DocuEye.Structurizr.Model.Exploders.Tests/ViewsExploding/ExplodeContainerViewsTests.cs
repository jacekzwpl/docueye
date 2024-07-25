using DocuEye.ViewsKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ViewsExploding
{
    public class ExplodeContainerViewsTests : BaseExploderTests
    {
        [Test]
        public void WhenContainerViewExistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var views = new List<StructurizrContainerView>
            {
                new StructurizrContainerView
                {
                    Title = "Container View",
                    Description = "This is a Container View",
                    Key = "ContainerView",
                    PaperSize = "A4",
                    SoftwareSystemId = "1",
                    ExternalSoftwareSystemBoundariesVisible = true,
                    AutomaticLayout = new StructurizrAutomaticLayout
                    {
                        Implementation = "Hierarchical",
                        RankDirection = "TopBottom",
                        RankSeparation = 300,
                        NodeSeparation = 300,
                        EdgeSeparation = 300,
                        Vertices = true
                    },
                    Elements = new List<StructurizrElementView>
                    {
                        new StructurizrElementView
                        {
                            Id = "1",
                            X = 100,
                            Y = 100
                        }
                    },
                    Relationships = new List<StructurizrRelationshipView>
                    {
                        new StructurizrRelationshipView
                        {
                            Id = "1"
                        }
                    }
                }
            };
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeContainerViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ViewType, Is.EqualTo(ViewType.ContainerView));
            Assert.That(result.First().Title, Is.EqualTo("Container View"));
            Assert.That(result.First().Description, Is.EqualTo("This is a Container View"));
            Assert.That(result.First().Key, Is.EqualTo("ContainerView"));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().AutomaticLayout?.Implementation, Is.EqualTo("Hierarchical"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
            Assert.That(result.First().AutomaticLayout?.RankSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.NodeSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.EdgeSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.Vertices, Is.True);
            Assert.That(result.First().Elements.Count(), Is.EqualTo(1));
            Assert.That(result.First().Relationships.Count(), Is.EqualTo(1));
            Assert.That(result.First().ExternalBoundariesVisible, Is.True);
            Assert.That(result.First().Elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().Elements.First().X, Is.EqualTo(100));
            Assert.That(result.First().Elements.First().Y, Is.EqualTo(100));
            Assert.That(result.First().Relationships.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().StructurizrElementId, Is.EqualTo("1"));
        }

        [Test]
        public void WhenMultipleContainerViewsExistsThenAllViewsAreExploded()
        {
            // Arrange
            var views = new List<StructurizrContainerView>
            {
                new StructurizrContainerView
                {
                    Title = "Container View 1",
                    Description = "This is a Container View 1",
                    Key = "ContainerView1",
                    PaperSize = "A4",
                    ExternalSoftwareSystemBoundariesVisible = true,
                    AutomaticLayout = new StructurizrAutomaticLayout
                    {
                        Implementation = "Hierarchical",
                        RankDirection = "TopBottom",
                        RankSeparation = 300,
                        NodeSeparation = 300,
                        EdgeSeparation = 300,
                        Vertices = true
                    },
                    Elements = new List<StructurizrElementView>
                    {
                        new StructurizrElementView
                        {
                            Id = "1",
                            X = 100,
                            Y = 100
                        }
                    },
                    Relationships = new List<StructurizrRelationshipView>
                    {
                        new StructurizrRelationshipView
                        {
                            Id = "1"
                        }
                    }
                },
                new StructurizrContainerView
                {
                    Title = "Container View 2",
                    Description = "This is a Container View 2",
                    Key = "ContainerView2",
                    PaperSize = "A4",
                    ExternalSoftwareSystemBoundariesVisible = true,
                    AutomaticLayout = new StructurizrAutomaticLayout
                    {
                        Implementation = "Hierarchical",
                        RankDirection = "TopBottom",
                        RankSeparation = 300,
                        NodeSeparation = 300,
                        EdgeSeparation = 300,
                        Vertices = true
                    },
                    Elements = new List<StructurizrElementView>
                    {
                        new StructurizrElementView
                        {
                            Id = "1",
                            X = 100,
                            Y = 100
                        }
                    },
                    Relationships = new List<StructurizrRelationshipView>
                    {
                        new StructurizrRelationshipView
                        {
                            Id = "1"
                        }
                    }
                }
            };
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeContainerViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
