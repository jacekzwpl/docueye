using DocuEye.Structurizr.Json.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ViewsExploding
{
    public class ExplodeViewConfigurationTests : BaseExploderTests
    {
        [Test]
        public void WhenElementStylesExistsThenAllPropertiesAreMatched()
        {
            var configuration = new StructurizrJsonConfiguration
            {
                Styles = new StructurizrJsonConfigurationStyles
                {
                    Elements = new List<StructurizrJsonElementStyle>
                    {
                        new StructurizrJsonElementStyle
                        {
                            Tag = "Tag",
                            Background = "#FFFFFF",
                            Color = "#000000",
                            Opacity = 100,
                            Width = 1,
                            Shape = "Rectangle",
                            Icon = "Icon",
                            Border = "Border",
                            Stroke = "Stroke",
                            StrokeWidth = "StrokeWidth",
                            FontSize = 12,
                            Height = 1,
                            Metadata = true
                        }
                    }
                }
            };
            var exploder = new ViewsExploder();

            var result = exploder.ExplodeViewConfiguration(configuration);

            Assert.That(result.ElementStyles.Count(), Is.EqualTo(1));
            Assert.That(result.ElementStyles.First().Tag, Is.EqualTo("Tag"));
            Assert.That(result.ElementStyles.First().Background, Is.EqualTo("#FFFFFF"));
            Assert.That(result.ElementStyles.First().Color, Is.EqualTo("#000000"));
            Assert.That(result.ElementStyles.First().Opacity, Is.EqualTo(100));
            Assert.That(result.ElementStyles.First().Width, Is.EqualTo(1));
            Assert.That(result.ElementStyles.First().Shape, Is.EqualTo("Rectangle"));
            Assert.That(result.ElementStyles.First().Icon, Is.EqualTo("Icon"));
            Assert.That(result.ElementStyles.First().Border, Is.EqualTo("Border"));
            Assert.That(result.ElementStyles.First().Stroke, Is.EqualTo("Stroke"));
            Assert.That(result.ElementStyles.First().StrokeWidth, Is.EqualTo("StrokeWidth"));
            Assert.That(result.ElementStyles.First().FontSize, Is.EqualTo(12));
            Assert.That(result.ElementStyles.First().Height, Is.EqualTo(1));
            Assert.That(result.ElementStyles.First().Metadata, Is.True);


        }

        [Test]
        public void WhenRelationshipStylesExistsThenAllProperitesAreMatched()
        {
            var configuration = new StructurizrJsonConfiguration
            {
                Styles = new StructurizrJsonConfigurationStyles
                {
                    Relationships = new List<StructurizrJsonRelationshipStyle>
                    {
                        new StructurizrJsonRelationshipStyle
                        {
                            Tag = "Tag",
                            Color = "#000000",
                            Opacity = 100,
                            Width = 1,
                            Dashed = true,
                            Routing = "Direct",
                            Position = 1,
                            FontSize = 12,
                            Thickness = 1
                        }
                    }
                }
            };

            var exploder = new ViewsExploder();
            var result = exploder.ExplodeViewConfiguration(configuration);

            Assert.That(result.RelationshipStyles.Count(), Is.EqualTo(1));
            Assert.That(result.RelationshipStyles.First().Tag, Is.EqualTo("Tag"));
            Assert.That(result.RelationshipStyles.First().Color, Is.EqualTo("#000000"));
            Assert.That(result.RelationshipStyles.First().Opacity, Is.EqualTo(100));
            Assert.That(result.RelationshipStyles.First().Width, Is.EqualTo(1));
            Assert.That(result.RelationshipStyles.First().Dashed, Is.True);
            Assert.That(result.RelationshipStyles.First().Routing, Is.EqualTo("Direct"));
            Assert.That(result.RelationshipStyles.First().Position, Is.EqualTo(1));
            Assert.That(result.RelationshipStyles.First().FontSize, Is.EqualTo(12));
            Assert.That(result.RelationshipStyles.First().Thickness, Is.EqualTo(1));

        }

        [Test]
        public void WhenTerminologyExistsThenAllProperitesAreMatched()
        {
            var configuration = new StructurizrJsonConfiguration()
            {
                Terminology = new StructurizrJsonTerminology()
                {
                    Code = "Code",
                    Enterprise = "Enterprise",
                    Person = "Person",
                    SoftwareSystem = "SoftwareSystem",
                    Container = "Container",
                    Component = "Component",
                    Relationship = "Relationship",
                    DeploymentNode = "DeploymentNode",
                    InfrastructureNode = "InfrastructureNode"
                }
            };

            var exploder = new ViewsExploder();
            var result = exploder.ExplodeViewConfiguration(configuration);

            Assert.That(result.Terminology?.Code, Is.EqualTo("Code"));
            Assert.That(result.Terminology?.Enterprise, Is.EqualTo("Enterprise"));
            Assert.That(result.Terminology?.Person, Is.EqualTo("Person"));
            Assert.That(result.Terminology?.SoftwareSystem, Is.EqualTo("SoftwareSystem"));
            Assert.That(result.Terminology?.Container, Is.EqualTo("Container"));
            Assert.That(result.Terminology?.Component, Is.EqualTo("Component"));
            Assert.That(result.Terminology?.Relationship, Is.EqualTo("Relationship"));
            Assert.That(result.Terminology?.DeploymentNode, Is.EqualTo("DeploymentNode"));
            Assert.That(result.Terminology?.InfrastructureNode, Is.EqualTo("InfrastructureNode"));

        }

        [Test]
        public void WhenThemesExistsThenAllProperitesAreMatched()
        {
            var configuration = new StructurizrJsonConfiguration()
            {
                Themes = new List<string>
                {
                    "Theme1",
                    "Theme2"
                }
            };

            var exploder = new ViewsExploder();
            var result = exploder.ExplodeViewConfiguration(configuration);

            Assert.That(result.Themes.Count(), Is.EqualTo(2));
            Assert.That(result.Themes.First(), Is.EqualTo("Theme1"));
            Assert.That(result.Themes.Last(), Is.EqualTo("Theme2"));
        }
    }
}
