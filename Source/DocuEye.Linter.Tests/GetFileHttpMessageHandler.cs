using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DocuEye.Linter.Tests
{
    public class GetFileHttpMessageHandler : HttpMessageHandler
    {
        private string filePath;
        private string contentType;

        public GetFileHttpMessageHandler(string filePath, string contentType = "text/plain")
        {
            this.filePath = filePath;
            this.contentType = contentType;
        }

        sealed protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {

            string text = File.ReadAllText(filePath);
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(text, Encoding.UTF8, contentType),
            };

            return Task.FromResult(response);
        }
    }
}
