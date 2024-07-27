using DocuEye.ViewsKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ViewsExploding
{
    public class ExplodeImageViewsTests : BaseExploderTests
    {
        [Test]
        public void WhenImageViewExistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var views = new List<StructurizrImageView>
            {
                new StructurizrImageView
                {
                    Title = "Image View",
                    Description = "This is an Image View",
                    Key = "ImageView",
                    ElementId = "1",
                    ContentType = "image/png",
                    Content = "base64",
                }
            };
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeImageViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().ViewType, Is.EqualTo(ViewType.ImageView));
            Assert.That(result.First().Title, Is.EqualTo("Image View"));
            Assert.That(result.First().Description, Is.EqualTo("This is an Image View"));
            Assert.That(result.First().Key, Is.EqualTo("ImageView"));
            Assert.That(result.First().StructurizrElementId, Is.EqualTo("1"));
            Assert.That(result.First().ContentType, Is.EqualTo("image/png"));
            Assert.That(result.First().Content, Is.EqualTo("base64"));
        }

        [Test]
        public void WhenMultipleImageViewsExistsThenAllViewsAreExploded()
        {
            // Arrange
            var views = new List<StructurizrImageView>
            {
                new StructurizrImageView
                {
                    Title = "Image View 1",
                    Description = "This is an Image View 1",
                    Key = "ImageView1",
                    ElementId = "1",
                    ContentType = "image/png",
                    Content = "base64",
                },
                new StructurizrImageView
                {
                    Title = "Image View 2",
                    Description = "This is an Image View 2",
                    Key = "ImageView2",
                    ElementId = "2",
                    ContentType = "image/png",
                    Content = "base64",
                }
            };
            var exploder = new ViewsExploder(this.mapper);

            // Act
            var result = exploder.ExplodeImageViews(views);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
