using DocuEye.CLI.ApiClient.Model;
using DocuEye.Infrastructure.Tests.Http;
using DocuEye.Structurizr.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System.Net;
using System.Text;
using System.Text.Json;

namespace DocuEye.CLI.ApiClient.Tests
{
    public class DocuEyeApiClientTests
    {


        [SetUp]
        public void Setup()
        {
        }

       



        [Test]
        public async Task WhenImportWorkspaceThenResultHasSuccessStatus()
        {
            // Arrange



            Assert.Fail();
            /*
            // Arrange
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize<ImportWorkspaceResponse>(new ImportWorkspaceResponse()
                {
                    WorkspaceId = "testworkspaceId",
                    IsSuccess = true,
                }), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            fakeHttpClient.BaseAddress = new Uri("https://docueye.com");

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
            */
        }


        [Test]
        public async Task WhenImportWorkspaceEndpointNotReturnsSuccessStatusCodeThenResultHasErrorMessageAndFailureStatus()
        {
            Assert.Fail();
            /*
            // Arrange
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonSerializer.Serialize(new
                {
                    Title = "test problem title",
                    Status = 400,
                    Detail = "Test error description"
                }), Encoding.UTF8, "application/problem+json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            fakeHttpClient.BaseAddress = new Uri("https://docueye.com");

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
            Assert.That(result.IsSuccess, Is.EqualTo(false), "Request should return success status");
            Assert.That(result.Message?.Length, Is.GreaterThan(0), "Request should return message with content");
            */
        }




    }
}