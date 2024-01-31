using DocuEye.CLI.ApiClient.Model;
using DocuEye.Infrastructure.Tests.Http;
using DocuEye.Structurizr.Model;
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

            // Arrange
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize<ImportWorkspaceResult>(new ImportWorkspaceResult()
                {
                    WorkspaceId = "testworkspaceId",
                    IsSuccess = true,
                }), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            var request = new ImportWorkspaceRequest()
            {
                ImportKey = "test",
                WorkspaceData = new StructurizrWorkspace()
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