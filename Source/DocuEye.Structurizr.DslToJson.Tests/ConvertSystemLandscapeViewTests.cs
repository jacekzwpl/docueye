
using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DslToJson.Tests
{
    public class ConvertSystemLandscapeViewTests
    {
        private readonly StructurizrWorkspace workspace = new StructurizrWorkspace()
        {
            Model = new StructurizrModel() { 
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("pid0","person0")
                    {
                        Name = "Person0",
                        Type = StructurizrModelElementType.Person,
                        Properties = new Dictionary<string, string>()
                        {
                            { "key1", "valueSoftwareSystem1" },
                            { "key2", "value2" }
                        },
                        Tags = new List<string>() { "tagSoftwareSystem1", "tag2" }
                    },
                    new StructurizrModelElement("id0","identifier0")
                    {
                        Name = "SoftwareSystem0",
                        Type = StructurizrModelElementType.SoftwareSystem,
                        Properties = new Dictionary<string, string>()
                        {
                            { "key1", "valueSoftwareSystem1" },
                            { "key2", "value2" }
                        },
                        Tags = new List<string>() { "tagSoftwareSystem1", "tag2" }
                    },
                    new StructurizrModelElement("id1","identifier1")
                    {
                        Name = "SoftwareSystem1",
                        Type = StructurizrModelElementType.SoftwareSystem,
                        Technology = "test",
                        Properties = new Dictionary<string, string>()
                        {
                            { "key1", "value1" },
                            { "key2", "value2" }
                        },
                        Tags = new List<string>() { "tag1", "tag2" }
                    },
                    new StructurizrModelElement("id2","identifier2")
                    {
                        Name = "SoftwareSystem2",
                        Type = StructurizrModelElementType.SoftwareSystem,
                        Technology = "test2",
                        Properties = new Dictionary<string, string>()
                        {
                            { "key1", "valueComponent1" },
                            { "key2", "value2" }
                        }
                        ,
                        Tags = new List<string>() { "Component1", "tag2" }
                    },
                    new StructurizrModelElement("elid0","elementid0")
                    {
                        Name = "Element0",
                        Type = StructurizrModelElementType.CustomElement,
                        Technology = "test2",
                        Properties = new Dictionary<string, string>()
                        {
                            { "key1", "valueComponent1" },
                            { "key2", "value2" }
                        }
                        ,
                        Tags = new List<string>() { "Component1", "tag2" }
                    }

                },
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("id0", new string[] { "tag1" })
                    {
                        ModelId = "1",
                        SourceIdentifier = "identifier0",
                        DestinationIdentifier = "identifier1",
                    },
                    new StructurizrRelationship("id1", new string[] { "tag2" })
                    {
                        ModelId = "2",
                        SourceIdentifier = "identifier1",
                        DestinationIdentifier = "identifier2",
                    }
                }
            }
        };
        [Test]
        public void WhenViewIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrSystemLandscapeView("SystemLandscapeView", "Test Description")
            {
                Title = "Test Title"
            };
            // Act
            var result = workspaceConverter.ConvertSystemLandscapeView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo("SystemLandscapeView"));
            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
        }

        [Test]
        public void WhenViewWithIncludeElementsIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrSystemLandscapeView("SystemLandscapeView", "Test Description")
            {
                Include = new string[] { "element.type == Person" }
            };
            // Act
            var result = workspaceConverter.ConvertSystemLandscapeView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Elements?.Count(), Is.EqualTo(1));
            Assert.That(result.Elements.First().Id, Is.EqualTo("pid0"));
            Assert.That(result.Relationships?.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenViewWithExcludeElementsIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrSystemLandscapeView("SystemLandscapeView", "Test Description")
            {
                Include = new string[] { "*" },
                Exclude = new string[] { "element.type == SoftwareSystem" }
            };
            // Act
            var result = workspaceConverter.ConvertSystemLandscapeView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Elements?.Count(), Is.EqualTo(2));
            Assert.That(result.Elements.First().Id, Is.EqualTo("pid0"));
            Assert.That(result.Elements.Last().Id, Is.EqualTo("elid0"));
            Assert.That(result.Relationships?.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenViewWithIncludeAndExcludeElementsIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrSystemLandscapeView("SystemLandscapeView", "Test Description")
            {
                Include = new string[] { "element.tag == tag2" },
                Exclude = new string[] { "element.tag == tag1" }
            };
            // Act
            var result = workspaceConverter.ConvertSystemLandscapeView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Elements?.Count(), Is.EqualTo(4));
            Assert.That(result.Relationships?.Count(), Is.EqualTo(0));
        }

        [Test]
        public void WhenViewOnlyWithRelationshipExpressionIsDefindedThenValidJsonIsReturned() {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrSystemLandscapeView("SystemLandscapeView", "Test Description")
            {
                Include = new string[] { "*" },
                Exclude = new string[] { "relationship.tag == tag1" }
            };
            // Act
            var result = workspaceConverter.ConvertSystemLandscapeView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Elements?.Count(), Is.EqualTo(5));
            Assert.That(result.Relationships?.Count(), Is.EqualTo(1));
        }
    }
}
