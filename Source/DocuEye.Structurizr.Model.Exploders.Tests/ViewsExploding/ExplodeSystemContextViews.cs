using DocuEye.Structurizr.Json.Model;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ViewsExploding
{
    public class ExplodeSystemContextViews : BaseExploderTests
    {
        [Test]
        public void WhenSystemContextViewExistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var views = new List<StructurizrJsonSystemContextView>
            {
                new StructurizrJsonSystemContextView
                {
                    Title = "System Context View",
                    Description = "This is a System Context View",
                    Key = "SystemContextView",
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
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeSystemContextViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ViewType, Is.EqualTo(ViewType.SystemContextView));
            Assert.That(result.First().Title, Is.EqualTo("System Context View"));
            Assert.That(result.First().Description, Is.EqualTo("This is a System Context View"));
            Assert.That(result.First().Key, Is.EqualTo("SystemContextView"));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().AutomaticLayout?.Implementation, Is.EqualTo("Hierarchical"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
            Assert.That(result.First().AutomaticLayout?.RankSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.NodeSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.EdgeSeparation, Is.EqualTo(300));
            Assert.That(result.First().AutomaticLayout?.Vertices, Is.True);
            Assert.That(result.First().Elements.Count(), Is.EqualTo(1));
            Assert.That(result.First().Elements.First().StructurizrId, Is.EqualTo("1"));
        }

        [Test]
        public void WhenMultipleSystemContextViewsExistsThenAllViewsAreExploded()
        {
            // Arrange
            var views = new List<StructurizrJsonSystemContextView>
            {
                new StructurizrJsonSystemContextView
                {
                    Title = "System Context View",
                    Description = "This is a System Context View",
                    Key = "SystemContextView",
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
                new StructurizrJsonSystemContextView
                {
                    Title = "System Context View 2",
                    Description = "This is a System Context View 2",
                    Key = "SystemContextView2",
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
            var result = exploder.ExplodeSystemContextViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
