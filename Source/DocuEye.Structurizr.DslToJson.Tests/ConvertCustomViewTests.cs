using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DslToJson.Tests
{
    public class ConvertCustomViewTests
    {
        private readonly StructurizrWorkspace workspace = new StructurizrWorkspace()
        {
            Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("el1","element1")
                    {
                        Name = "element1",
                        Type = StructurizrModelElementType.CustomElement
                    },
                    new StructurizrModelElement("el2","element2")
                    {
                        Name = "element2",
                        Type = StructurizrModelElementType.CustomElement
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
                    }
                    


                },
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("id0", new string[] { "tag1" })
                    {
                        ModelId = "1",
                        SourceIdentifier = "element1",
                        DestinationIdentifier = "element1",
                    },
                    new StructurizrRelationship("id1", new string[] { "tag2" })
                    {
                        ModelId = "2",
                        SourceIdentifier = "element1",
                        DestinationIdentifier = "identifier0",
                    }
                }
            }

        };

        [Test]
        public void WhenViewIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrCustomView("Custom", "Test Title", "Test Description")
            {
                Include = new string[] { "*" }
            };
            // Act
            var result = workspaceConverter.ConvertCustomView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo("Custom"));
            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.Elements, Is.Not.Null);
            Assert.That(result.Elements.Count(), Is.EqualTo(2));
            Assert.That(result.Relationships?.Count(), Is.EqualTo(1));
        }
    }
}
