using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DslToJson.Tests
{
    public class ConvertFilteredViewTests
    {
        [Test]
        public void WhenViewIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(new StructurizrWorkspace(), "");
            var view = new StructurizrFilteredView("FilteredKey", "BaseKey")
            {
                Title = "Test Title",
                FilterMode = "exclude",
                Tags = new List<string> { "tag1", "tag2" },
                Description = "Test Description"
            };
            // Act
            var result = workspaceConverter.ConvertFilteredView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Key, Is.EqualTo("FilteredKey"));
            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.Mode, Is.EqualTo("exclude"));
            Assert.That(result.Tags, Is.Not.Null);
            Assert.That(result.Tags.Count(), Is.EqualTo(2));
        }
    }
}
