using DocuEye.CLI.ApiClient.Model;
using DocuEye.Infrastructure.Tests.Http;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DocuEye.CLI.ApiClient.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task WhenImportWorkspaceThenResultHasSuccessStatus()
        {
            Assert.Fail();

            // Arrange
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize<ImportWorkspaceResult>(new ImportWorkspaceResult()), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            var request = new ImportWorkspaceRequest()
            {

            };


            // Act
            var client = new DocuEyeApiClient(fakeHttpClient);
            var result = await client.ImportWorkspace(request);

            // Assert
            Assert.That(result, !Is.Null, "Request should return object");
            Assert.That(result.IsSuccess, Is.EqualTo(true), "Request should return success status");
        }

      


    }
}