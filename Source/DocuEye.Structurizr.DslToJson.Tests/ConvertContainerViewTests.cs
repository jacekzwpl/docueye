using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DslToJson.Tests
{
    public class ConvertContainerViewTests
    {
        private readonly StructurizrWorkspace workspace = new StructurizrWorkspace()
        {
            Model = new StructurizrModel()
            {
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
                    new StructurizrModelElement("c0","container0")
                    {
                        Name = "Container0",
                        ParentIdentifier = "identifier0",
                        Type = StructurizrModelElementType.Container,
                        Technology = "test",
                        Properties = new Dictionary<string, string>()
                        {
                            { "key1", "value1" },
                            { "key2", "value2" }
                        },
                        Tags = new List<string>() { "tag1", "tag2" }
                    },
                    new StructurizrModelElement("c1","container1")
                    {
                        Name = "Container1",
                        ParentIdentifier = "identifier1",
                        Type = StructurizrModelElementType.Container,
                        Technology = "test",
                        Properties = new Dictionary<string, string>()
                        {
                            { "key1", "value1" },
                            { "key2", "value2" }
                        },
                        Tags = new List<string>() { "tag1", "tag2" }
                    }


                },
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("id0", new string[] { "tag1" })
                    {
                        ModelId = "1",
                        SourceIdentifier = "container0",
                        DestinationIdentifier = "identifier1",
                    },
                    new StructurizrRelationship("id1", new string[] { "tag2" })
                    {
                        ModelId = "2",
                        SourceIdentifier = "container0",
                        DestinationIdentifier = "container1",
                    },
                    new StructurizrRelationship("id2", new string[] { "tag1" })
                    {
                        ModelId = "3",
                        SourceIdentifier = "person0",
                        DestinationIdentifier = "container0",
                    },
                }
            }

        };

        [Test]
        public void WhenViewIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrContainerView("identifier0", "Containers0", "Test Description")
            {
                Title = "Test Title",
                Include = new string[] { "*" }
            };
            // Act
            var result = workspaceConverter.ConvertContainerView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SoftwareSystemId, Is.EqualTo("id0"));
            Assert.That(result.Key, Is.EqualTo("Containers0"));
            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.Elements, Is.Not.Null);
            Assert.That(result.Elements.Count(), Is.EqualTo(3));
            Assert.That(result.Relationships?.Count(), Is.EqualTo(2));
        }
    }
}
