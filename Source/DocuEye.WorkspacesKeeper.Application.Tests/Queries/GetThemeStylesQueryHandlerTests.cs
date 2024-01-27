using DocuEye.WorkspacesKeeper.Application.Queries.GetThemeStyles;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using Moq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DocuEye.WorkspacesKeeper.Application.Tests.Queries
{
    public class GetThemeStylesQueryHandlerTests : BaseWorkspacesKeeperTests
    {
        [Test]
        public async Task WhenHandeGetThemeStylesQueryThenThemeStylesResultIsReturned()
        {
            // Arrange
            var theme = new ThemeStylesResult()
            {
                Elements = new List<ElementStyle>()
            };
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize<ThemeStylesResult>(new ThemeStylesResult()), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(fakeHttpClient);

            var query = new GetThemeStylesQuery("http://docueye.com");

            // Act
            var handler = new GetThemeStylesQueryHandler(httpClientFactoryMock.Object);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, !Is.Null, "Hanlder should return object");
        }

        [Test]
        public async Task WhenThemsUrlNotWorkingThenNullIsReturned()
        {
            // Arrange
            var theme = new ThemeStylesResult()
            {
                Elements = new List<ElementStyle>()
            };
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(JsonSerializer.Serialize<ThemeStylesResult>(new ThemeStylesResult()), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(fakeHttpClient);

            var query = new GetThemeStylesQuery("http://docueye.com");

            // Act
            var handler = new GetThemeStylesQueryHandler(httpClientFactoryMock.Object);
            var item = await handler.Handle(query, default);

            // Assert
            Assert.That(item, Is.Null, "Hanlder should return null.");
        }
    }
}
