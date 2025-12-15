using DocuEye.Structurizr.Json.Model;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ViewsExploding
{
    public class ExplodeSystemLandscapeViewsTests : BaseExploderTests
    {
        [Test]
        public void WhenSystemLandscapeViewExiststThenAllProperitesAreMatched()
        {
            // Arrange
            var views = new List<StructurizrJsonSystemLandscapeView>
            {
                new StructurizrJsonSystemLandscapeView
                {
                    Title = "System Landscape View",
                    Description = "This is a System Landscape View",
                    Key = "SystemLandscapeView",
                    PaperSize = "A4",
                    EnterpriseBoundaryVisible = true,
                    AutomaticLayout = new StructurizrJsonAutomaticLayout
                    {
                        Implementation = "Hierarchical",
                        RankDirection = "TopBottom",
                        RankSeparation = 300,
                        NodeSeparation = 300,
                        EdgeSeparation = 300,
                        Vertices = true
                    },
                    Elements = new List<StructurizrJsonElementView>
                    {
                        new StructurizrJsonElementView
                        {
                            Id = "1",
                            X = 100,
                            Y = 100
                        }
                    },
                    Relationships = new List<StructurizrJsonRelationshipView>
                    {
                        new StructurizrJsonRelationshipView
                        {
                            Id = "1"
                        }
                    }
                }
            };
            var exploder = new ViewsExploder();

            // Act
            var result = exploder.ExplodeSystemLandscapeViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ViewType, Is.EqualTo(ViewType.SystemLandscapeView));
            Assert.That(result.First().Title, Is.EqualTo("System Landscape View"));
            Assert.That(result.First().Description, Is.EqualTo("This is a System Landscape View"));
            Assert.That(result.First().Key, Is.EqualTo("SystemLandscapeView"));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().ExternalBoundariesVisible, Is.True);
            Assert.That(result.First().AutomaticLayout?.Implementation, Is.EqualTo("Hierarchical"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
            Assert.That(result.First().AutomaticLayout?.RankSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.NodeSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.EdgeSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.Vertices, Is.True);
            Assert.That(result.First().Elements.Count(), Is.EqualTo(1));
            Assert.That(result.First().Elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().Elements.First().X, Is.EqualTo(100));
            Assert.That(result.First().Elements.First().Y, Is.EqualTo(100));
            Assert.That(result.First().Relationships.Count(), Is.EqualTo(1));
            Assert.That(result.First().Relationships.First().StructurizrId, Is.EqualTo("1"));

        }

        [Test]
        public void WhenMultipleLandscapeViewsExistsThenAllViewsAreExploded()
        {             
            // Arrange
            var views = new List<StructurizrJsonSystemLandscapeView>
            {
                new StructurizrJsonSystemLandscapeView
                {
                    Title = "System Landscape View 1",
                    Description = "This is a System Landscape View 1",
                    Key = "SystemLandscapeView1",
                    PaperSize = "A4",
                    EnterpriseBoundaryVisible = true,
                    AutomaticLayout = new StructurizrJsonAutomaticLayout
                    {
                        Implementation = "Hierarchical",
                        RankDirection = "TopBottom",
                        RankSeparation = 300,
                        NodeSeparation = 300,
                        EdgeSeparation = 300,
                        Vertices = true
                    },
                    Elements = new List<StructurizrJsonElementView>
                    {
                        new StructurizrJsonElementView
                        {
                            Id = "1",
                            X = 100,
                            Y = 100
                        }
                    },
                    Relationships = new List<StructurizrJsonRelationshipView>
                    {
                        new StructurizrJsonRelationshipView
                        {
                            Id = "1"
                        }
                    }
                },
                new StructurizrJsonSystemLandscapeView
                {
                    Title = "System Landscape View 2",
                    Description = "This is a System Landscape View 2",
                    Key = "SystemLandscapeView2",
                    PaperSize = "A4",
                    EnterpriseBoundaryVisible = true,
                    AutomaticLayout = new StructurizrJsonAutomaticLayout
                    {
                        Implementation = "Hierarchical",
                        RankDirection = "TopBottom",
                        RankSeparation = 300,
                        NodeSeparation = 300,
                        EdgeSeparation = 300,
                        Vertices = true
                    },
                    Elements = new List<StructurizrJsonElementView>
                    {
                        new StructurizrJsonElementView
                        {
                            Id = "1",
                            X = 100,
                            Y = 100
                        }
                    },
                    Relationships = new List<StructurizrJsonRelationshipView>
                    {
                        new StructurizrJsonRelationshipView
                        {
                            Id = "1"
                        }
                    }
                }
            };
            var exploder = new ViewsExploder();

            // Act
            var result = exploder.ExplodeSystemLandscapeViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

    }
}
