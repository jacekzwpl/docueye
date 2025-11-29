using DocuEye.Structurizr.Json.Model;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ViewsExploding
{
    public class ExplodeDynamicViewsTests : BaseExploderTests
    {
        [Test]
        public void WhenDynamicViewExistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var views = new List<StructurizrJsonDynamicView>
            {
                new StructurizrJsonDynamicView
                {
                    Title = "Dynamic View",
                    Description = "This is a Dynamic View",
                    Key = "DynamicView",
                    PaperSize = "A4",
                    ElementId = "1",
                    ExternalBoundariesVisible = true,
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
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeDynamicViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ViewType, Is.EqualTo(ViewType.DynamicView));
            Assert.That(result.First().Title, Is.EqualTo("Dynamic View"));
            Assert.That(result.First().Description, Is.EqualTo("This is a Dynamic View"));
            Assert.That(result.First().Key, Is.EqualTo("DynamicView"));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().StructurizrElementId, Is.EqualTo("1"));
            Assert.That(result.First().ExternalBoundariesVisible, Is.True);
            Assert.That(result.First().AutomaticLayout?.Implementation, Is.EqualTo("Hierarchical"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
            Assert.That(result.First().AutomaticLayout?.RankSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.NodeSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.EdgeSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.Vertices, Is.True);
            Assert.That(result.First().Elements.Count(), Is.EqualTo(1));
            Assert.That(result.First().Relationships.Count(), Is.EqualTo(1));
            Assert.That(result.First().Elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().Elements.First().X, Is.EqualTo(100));
            Assert.That(result.First().Elements.First().Y, Is.EqualTo(100));
            Assert.That(result.First().Relationships.First().StructurizrId, Is.EqualTo("1"));
           
           
        }

        [Test]
        public void WhenMultipleDynamicViewsExistsThenAllViewsAreExploded() 
        {
            // Arrange
            var views = new List<StructurizrJsonDynamicView>
            {
                new StructurizrJsonDynamicView
                {
                    Title = "Dynamic View 1",
                    Description = "This is a Dynamic View 1",
                    Key = "DynamicView1",
                    PaperSize = "A4",
                    ElementId = "1",
                    ExternalBoundariesVisible = true,
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
                new StructurizrJsonDynamicView
                {
                    Title = "Dynamic View 2",
                    Description = "This is a Dynamic View 2",
                    Key = "DynamicView2",
                    PaperSize = "A4",
                    ElementId = "2",
                    ExternalBoundariesVisible = true,
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
                            Id = "2",
                            X = 200,
                            Y = 200
                        }
                    },
                    Relationships = new List<StructurizrJsonRelationshipView>
                    {
                        new StructurizrJsonRelationshipView
                        {
                            Id = "2"
                        }
                    }
                }
            };
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeDynamicViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
