using DocuEye.Structurizr.Json.Model;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ViewsExploding
{
    public class ExplodeFilteredViewsTests : BaseExploderTests
    {
        [Test]
        public void WhenFilteredViewExistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var views = new List<StructurizrJsonFilteredView>
            {
                new StructurizrJsonFilteredView
                {
                    Title = "Filtered View",
                    Description = "This is a Filtered View",
                    Key = "FilteredView",
                    Mode = "Include",
                    BaseViewKey = "BaseView",
                    Tags = new List<string> { "Tag1", "Tag2" }
                }
            };
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeFilteredViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ViewType, Is.EqualTo(ViewType.FilteredView));
            Assert.That(result.First().Title, Is.EqualTo("Filtered View"));
            Assert.That(result.First().Description, Is.EqualTo("This is a Filtered View"));
            Assert.That(result.First().Key, Is.EqualTo("FilteredView"));
            Assert.That(result.First().Mode, Is.EqualTo("Include"));
            Assert.That(result.First().BaseViewKey, Is.EqualTo("BaseView"));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("Tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("Tag2"));
        }

        [Test]
        public void WhenMultipleFilteredViewsExistsThenAllViewsAreExploded()
        {
            // Arrange
            var views = new List<StructurizrJsonFilteredView>
            {
                new StructurizrJsonFilteredView
                {
                    Title = "Filtered View 1",
                    Description = "This is a Filtered View 1",
                    Key = "FilteredView1",
                    Mode = "Include",
                    BaseViewKey = "BaseView1",
                    Tags = new List<string> { "Tag1", "Tag2" }
                },
                new StructurizrJsonFilteredView
                {
                    Title = "Filtered View 2",
                    Description = "This is a Filtered View 2",
                    Key = "FilteredView2",
                    Mode = "Exclude",
                    BaseViewKey = "BaseView2",
                    Tags = new List<string> { "Tag3", "Tag4" }
                }
            };
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeFilteredViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
